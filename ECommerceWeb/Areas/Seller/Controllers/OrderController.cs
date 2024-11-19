using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ECommerceWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = StaticDetails.Role_Seller)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHubContext<OrderStatusChangedHub> _hubContext;

        public OrderController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager, IHubContext<OrderStatusChangedHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {

            string currentLoggedInUser = _userManager.GetUserId(User);

            List<OrderItem> orderItemList = _unitOfWork.OrderItem.GetAll(oi => oi.Product.SellerId == currentLoggedInUser, includeProperties: "Product,Order,Status").ToList();

            List<int> orderIds = orderItemList.Select(oi => oi.OrderId).Distinct().ToList();

            List<Order> orderList = _unitOfWork.Order.GetAll(o => orderIds.Contains(o.OrderId)).ToList();

            OrderVM orderVM = new OrderVM();
            if (orderList.Count > 0)
            {
                orderVM.orderItemsList = orderItemList;
                orderVM.OrderShippingAddresses = orderList.ToDictionary(o => o.OrderId, o => JsonSerializer.Deserialize<ShippingAddress>(o.AddressId))!;
            }
            return View(orderVM);
        }



        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int orderItemId, string status)
        {
            string currentLoggedInUser = _userManager.GetUserId(User);
            if (currentLoggedInUser == null)
            {
                return BadRequest(new { success = false, message = "Seller Is Not Logged In" });
            }
            List<OrderItemStatus> statusList = _unitOfWork.OrderItemStatus.GetAll().ToList();

            if (!statusList.Any(s => s.StatusName == status))
            {
                return BadRequest(new { success = false, message = "Status Not Found" });
            }
            var orderItem = _unitOfWork.OrderItem.Get(oi => oi.OrderItemId == orderItemId, includeProperties: "Order,Product");

            if (orderItem == null)
            {
                return BadRequest(new { success = false, message = "OrderItem Not Found" });
            }
            var currentStatus = _unitOfWork.OrderItemStatus.Get(s => s.StatusId == orderItem.StatusId)?.StatusName;
            if (currentStatus == "Delivered" || currentStatus == "Cancelled")
            {
                return BadRequest(new { success = false, message = $"Status cannot be changed as it is already {currentStatus}" });
            }

            OrderItemStatus OrderStatus = _unitOfWork.OrderItemStatus.Get(u => u.StatusName == status);
            orderItem.StatusId = OrderStatus.StatusId;
            _unitOfWork.OrderItem.Update(orderItem);
            _unitOfWork.Save();

            string customerId = orderItem.Order.customerId;
            await _hubContext.Clients.User(customerId).SendAsync("ReceiveStatusNotification", status, orderItem.OrderItemId, orderItem.Product.Name);
            await _hubContext.Clients.User(customerId).SendAsync("ReceiveStatusUpdate", status, orderItem.OrderItemId, orderItem.Product.Name);
            return Json(new { success = true });

        }
    }


}

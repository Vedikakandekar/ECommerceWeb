using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public OrderController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            string currentLoggedInUser = _userManager.GetUserId(User);

            List<OrderItem> orderItemList = _unitOfWork.OrderItem.GetAll(oi => oi.Product.SellerId == currentLoggedInUser, includeProperties: "Product,Order" ).ToList();

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
    }
}

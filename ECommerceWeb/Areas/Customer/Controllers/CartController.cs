using ECommerce.Data.Repository.IRepository;
using ECommerce.Models.ViewModels;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = StaticDetails.Role_Customer)]
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public CartController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            string currentLoggedInUSer = _userManager.GetUserId(User);
            if (currentLoggedInUSer == null)
            {
                return View("Login");
            }
            CartVM cartVM = new CartVM();
            cartVM.cart = _unitOfWork.Cart.Get(u => u.CustomerId == currentLoggedInUSer);
            if(cartVM.cart==null)
            {
                cartVM.cart = new Cart();
                return View(cartVM);
            }
            cartVM.cart.CartItems = _unitOfWork.CartItem.GetAll(u => u.CartId == cartVM.cart.CartId, includeProperties: "Product").ToList();
            cartVM.subtotal = (float)cartVM.cart.CartItems.Sum(item => item.Quantity * item.Price);
            cartVM.shippingFees = 50;
            cartVM.total = cartVM.subtotal + cartVM.shippingFees;
            cartVM.shippingAddress.CustomerId = currentLoggedInUSer;
            cartVM.addressList = _unitOfWork.ShippingAddress.GetAll(u => u.CustomerId == currentLoggedInUSer).ToList();
            return View(cartVM);
        }


        [HttpPost, ActionName("Index")]
        public IActionResult IndexPost(CartVM cartVM)
            {
            string currentLogedInUser = _userManager.GetUserId(User);
            if (currentLogedInUser == null || cartVM.cart.CartItems.Count==0)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.TotalAmount = (decimal)cartVM.total;
                order.customerId = currentLogedInUser;
                order.AddressId = JsonSerializer.Serialize(cartVM.shippingAddress);
                _unitOfWork.Order.Add(order);
                _unitOfWork.Save();

                Order newOrder = _unitOfWork.Order.Get(u=>u.OrderDate==order.OrderDate);
                 
             
                
                foreach(CartItem listItem in cartVM.cart.CartItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = newOrder.OrderId;
                    orderItem.Quantity = listItem.Quantity;
                    orderItem.UnitPrice = (decimal)listItem.Product.Price;
                    orderItem.TotalPrice = listItem.Quantity * (decimal)listItem.Product.Price;
                    orderItem.ProductId = listItem.ProductId;
                    orderItem.StatusId = _unitOfWork.OrderItemStatus.Get(u => u.StatusName == "Pending").StatusId;
                    _unitOfWork.OrderItem.Add(orderItem);
                    _unitOfWork.Save();
                    Products productToUpdate = _unitOfWork.Product.Get(u => u.Id == listItem.ProductId);
                    productToUpdate.stock = productToUpdate.stock - listItem.Quantity;
                    _unitOfWork.Product.Update(productToUpdate);
                    _unitOfWork.Save();

                }
                
               
                Cart cartToRemove = _unitOfWork.Cart.Get(u => u.CartId == cartVM.cart.CartId);

                _unitOfWork.CartItem.RemoveRane(_unitOfWork.CartItem.GetAll(u=>u.CartId==cartVM.cart.CartId));
                _unitOfWork.Save();
                _unitOfWork.Cart.Remove(cartToRemove);
                _unitOfWork.Save();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Error");
        }





        public IActionResult Delete(int productId)
        {
            string currentLogedInUser = _userManager.GetUserId(User);
            if (currentLogedInUser == null || productId == 0)
            {
                return View("Error");
            }
            
            Products product = _unitOfWork.Product.Get(u => u.Id == productId);
            if (product == null)
            {
                return View("Error");
            }
           CartItem itemToDelete =_unitOfWork.CartItem.Get(u => u.ProductId == productId);
            _unitOfWork.CartItem.Remove(itemToDelete);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAddress(int addressId)
        {
            string currentLogedInUser = _userManager.GetUserId(User);
            if (currentLogedInUser == null || addressId == 0)
            {
                return View("Error");
            }
            
            ShippingAddress address = _unitOfWork.ShippingAddress.Get(u => u.AddressId == addressId);
            if (address == null)
            {
                return View("Error");
            }
           
            _unitOfWork.ShippingAddress.Remove(address);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult AddAddress([FromBody] ShippingAddress model)
        {
            string currentLoggedInUSer = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(currentLoggedInUSer))
            {
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(model.CustomerId))
                {
                    model.CustomerId = currentLoggedInUSer;
                }
                _unitOfWork.ShippingAddress.Add(model);
                _unitOfWork.Save();
                return Json(new { success = true});
            }

            return  View("Error");
        }



    }
}

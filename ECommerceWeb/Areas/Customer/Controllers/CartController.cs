﻿using ECommerce.Data.Repository.IRepository;
using ECommerce.Models.ViewModels;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ECommerceWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
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
            cartVM.cart.CartItems = _unitOfWork.CartItem.GetAll(u => u.CartId == cartVM.cart.CartId, includeProperties: "Product").ToList();
            cartVM.subtotal = (float)cartVM.cart.CartItems.Sum(item => item.Quantity * item.Price);
            cartVM.shippingFees = 50;
            cartVM.total = cartVM.subtotal + cartVM.shippingFees;
            return View(cartVM);
        }

        public IActionResult Delete(int productId)
        {
            if (productId == 0)
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



    }
}

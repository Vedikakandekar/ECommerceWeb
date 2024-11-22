﻿using ECommerce.Data.Repository;
using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace ECommerceWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = StaticDetails.Role_Customer)]
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            string currentLoggedInUser = _userManager.GetUserId(User);
            if (currentLoggedInUser == null)
            {
                return View("Login");
            }
            List<Order> orderList = _unitOfWork.Order.GetAll(u=>u.customerId==currentLoggedInUser).ToList();
           // List<OrderItem> orderItemList = _unitOfWork.OrderItem.GetAll( oi=> orderList.Select(o => o.OrderId==oi.OrderId),"Product,Order").ToList();
            List<OrderItem> orderItemList = _unitOfWork.OrderItem.GetAll(oi => orderList.Select(o => o.OrderId).Contains(oi.OrderId), "Product,Order,Status").ToList();
            var orderShippingAddresses = orderList.ToDictionary(o => o.OrderId,o => JsonSerializer.Deserialize<ShippingAddress>(o.AddressId));

            OrderVM orderVM = new OrderVM();
           
            orderVM.orderItemsList = orderItemList;
            orderVM.OrderShippingAddresses = orderShippingAddresses;
            orderVM.StatusList= _unitOfWork.OrderItemStatus.GetAll().ToList().Select(x => x.StatusName).Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
            return View(orderVM);
        }
    }
}

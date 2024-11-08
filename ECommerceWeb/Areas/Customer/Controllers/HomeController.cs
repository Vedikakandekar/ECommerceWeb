using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerceWeb.Areas.Customer.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            IEnumerable<Products> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Details(int productId)
        {
            CartVM cartvm = new CartVM();
            Products product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");
            if (_userManager.GetUserId(User) == null)
            {
                cartvm.cart.CustomerId = "";

            }
            else
            {
                cartvm.cart.CustomerId = _userManager.GetUserId(User);
            }
            cartvm.item.ProductId = productId;
            cartvm.item.Product = product;
            cartvm.item.Quantity = 0;
            cartvm.item.Price = (decimal)product.Price;

            return View(cartvm);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int productId)
        {
            string currentLoggedInUser = _userManager.GetUserId(User);
            if (currentLoggedInUser == "" || currentLoggedInUser == null)
            {
                return View("Login");
            }
            else
            if (ModelState.IsValid)
            {

                Products product = _unitOfWork.Product.Get(u => u.Id == productId);

                if (product.Id == 0)
                {
                    return View("Error");
                }


                CartVM cartVM = new CartVM();
                cartVM.item.ProductId = product.Id;
                cartVM.item.Price = (decimal)product.Price;
                cartVM.item.Quantity = 1;
                cartVM.shippingFees = 5;
                Cart oldCart = _unitOfWork.Cart.Get(u => u.CustomerId == currentLoggedInUser);
                if (oldCart == null)
                {

                    //if cart is empty
                    cartVM.cart.CustomerId = _userManager.GetUserId(User);
                    cartVM.cart.CreatedAt = DateTime.Now;
                    cartVM.cart.IsActive = true;

                    _unitOfWork.Cart.Add(cartVM.cart);
                    _unitOfWork.Save();
                    Cart newCart = _unitOfWork.Cart.Get(u => u.CustomerId == cartVM.cart.CustomerId);
                   
                    if (newCart != null)
                    {
                        cartVM.item.CartId = newCart.CartId;
                        _unitOfWork.CartItem.Add(cartVM.item);
                        _unitOfWork.Save();
                    }
                    else
                        return View("Error");
                }
                else
                    if ( oldCart.CartId != 0)
                {
                    
                    cartVM.item.CartId = oldCart.CartId;
                    _unitOfWork.CartItem.Add(cartVM.item);
                    _unitOfWork.Save();
                }

                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }



    }
}

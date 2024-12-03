using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class LikesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public LikesController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            string currentLoggedInUser = _userManager.GetUserId(User);
            if(string.IsNullOrEmpty(currentLoggedInUser))
            {
                return View("Login");
            }
            AllProductsVM allProductsVM = new AllProductsVM();
         


            allProductsVM.ProductList= _unitOfWork.Likes.GetAll(u=>u.CustomerId == currentLoggedInUser, includeProperties:"product")
                .Select(like => like.product)
                .ToList();
            return View(allProductsVM);
        }
    }
}

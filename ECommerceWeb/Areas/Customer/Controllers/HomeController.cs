using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult GetProducts(string search = "",string category = "",string priceOrder = "",int page = 1,int pageSize = 8)
        {
            var productQuery = _unitOfWork.Product.GetAll(u => u.stock > 0, includeProperties: "Category");

            if (!string.IsNullOrEmpty(search))
                productQuery = productQuery.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(category) && category != "-Select Category-")
                productQuery = productQuery.Where(p => p.Category.Name == category);

            if (priceOrder == "low-to-high")
                productQuery = productQuery.OrderBy(p => p.Price);
            else if (priceOrder == "high-to-low")
                productQuery = productQuery.OrderByDescending(p => p.Price);

            var totalItems = productQuery.Count();
            var products = productQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Json(new
            {
                currentPage = page,
                totalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                products = products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    ImageUrl = p.ImageUrl.Replace("\\", "/"),
                    Category = p.Category.Name
                })
            });


        }
        public IActionResult Index()
        {
            var model = new AllProductsVM
            {
                ProductList = _unitOfWork.Product.GetAll().ToList(),
                CategoryList = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name"),
                CurrentPage = 1, 
                TotalPages = 5,  
                PageSize = 4     
            };

            return View(model);
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
            DetailsDTO dto = new DetailsDTO();

            Products product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");

            if (product != null)
            {
                dto.Product = product;
                CartItem itemList = _unitOfWork.CartItem.Get(u => u.Product.Id == product.Id, includeProperties: "Product");
                if(itemList != null)
                {
                    dto.IsInCart = true;
                }
                else
                {
                    dto.IsInCart = false;
                }
                    
                return View(dto);
            }
            return View("Error");
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
                    if (oldCart.CartId != 0)
                {

                    cartVM.item.CartId = oldCart.CartId;
                    _unitOfWork.CartItem.Add(cartVM.item);
                    _unitOfWork.Save();
                }

                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }

        public IActionResult SearchProduct(string searchString, string categoryFilter, string priceFilter)
        {
            if (string.IsNullOrWhiteSpace(searchString) && string.IsNullOrEmpty(categoryFilter) && string.IsNullOrEmpty(priceFilter))
            {
                return Json(new EmptyResult());
            }
            List<Products> ProductList = _unitOfWork.Product.GetAll(u => u.stock > 0, includeProperties: "Category").ToList();
            List<Products> FilteredProducts = ControllerHelper.FilterProductList(ProductList, searchString, categoryFilter,priceFilter);
            return Json(new { success = true, products = FilteredProducts });
        }


    }
}

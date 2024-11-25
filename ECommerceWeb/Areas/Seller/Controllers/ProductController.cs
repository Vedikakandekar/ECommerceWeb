using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Data.Repository;
using ECommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ECommerceWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = StaticDetails.Role_Seller)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            AllProductsVM productsVM = new AllProductsVM();
            productsVM.ProductList= _unitOfWork.Product.GetAll(u => u.SellerId == _userManager.GetUserId(User), includeProperties: "Category,").ToList();
            productsVM.CategoryList = _unitOfWork.Category.GetAll().ToList().Select(x => x.Name).Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
            return View(productsVM);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Products = new Products()
            };
            if (id == null || id == 0)
            {
                if (_userManager.GetUserId(User) != null)
                    productVM.Products.SellerId = _userManager.GetUserId(User)!;
                return View(productVM);
            }
            else
            {
                productVM.Products = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(obj.Products.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, obj.Products.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Products.ImageUrl = @"\images\product\" + fileName;
                }
                if (obj.Products.Id == 0)
                {

                    obj.Products.SellerId = _userManager.GetUserId(User);
                    _unitOfWork.Product.Add(obj.Products);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Created Successfully";
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    if (_userManager.GetUserId(User) == null)
                    {
                        return View("Error");
                    }
                    _unitOfWork.Product.Update(obj.Products);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Updated Successfully";
                    return RedirectToAction("Index", "Product");
                }
            }
            return View("Error");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            ProductVM productVM = new ProductVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Products = _unitOfWork.Product.Get(u => u.Id == id)
            };
            if (productVM.Products != null)
            {
                return View(productVM);
            }
            else
                return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Products? c = _unitOfWork.Product.Get(u => u.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(c);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index", "Product"); //parameters = action name, controller name
        }

        public IActionResult SearchSellerProduct(string searchString, string categoryFilter, string priceFilter)
        {
            string currentLoggedInSeller = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(currentLoggedInSeller))
                {
                return Json(new EmptyResult());
            }
            if (string.IsNullOrWhiteSpace(searchString) && string.IsNullOrEmpty(categoryFilter) && string.IsNullOrEmpty(priceFilter))
            {
                return Json(new EmptyResult());
            }
            List<Products> ProductList = _unitOfWork.Product.GetAll(u=>u.SellerId==currentLoggedInSeller, includeProperties: "Category").ToList();
            List<Products> FilteredProducts = ControllerHelper.FilterProductList(ProductList, searchString, categoryFilter, priceFilter);
            return Json(new { success = true, products = FilteredProducts });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Products> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        #endregion
    }
}

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
    public class ProductController
        (IUnitOfWork unitOfWork, 
        IWebHostEnvironment _webHostEnvironment,
        UserManager<IdentityUser> userManager) : Controller
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment =_webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager=userManager;

        public IActionResult Index()
        {
            List<Products> products = unitOfWork.Product.GetAll(u => u.SellerId == _userManager.GetUserId(User), includeProperties: "Category,").ToList();
            return View(products);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                CategoryList = unitOfWork.Category.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Products = new Products()
            };
            if (id == null || id==0)
            {
                
                return View(productVM);
            }
            else
            {
                productVM.Products = unitOfWork.Product.Get(u => u.Id == id);
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
                        unitOfWork.Product.Add(obj.Products);
                        unitOfWork.Save();
                        TempData["success"] = "Product Created Successfully";
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        unitOfWork.Product.Update(obj.Products);
                        unitOfWork.Save();
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
                CategoryList = unitOfWork.Category.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Products = unitOfWork.Product.Get(u => u.Id == id)
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
            Products? c = unitOfWork.Product.Get(u => u.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            unitOfWork.Product.Remove(c);
            unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index", "Product"); //parameters = action name, controller name

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Products> objProductList = unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        #endregion
    }



}

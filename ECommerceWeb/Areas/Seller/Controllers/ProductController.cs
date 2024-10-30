using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Data.Repository;

namespace ECommerceWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class ProductController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        public IActionResult Index()
        {
            List<Products> categories = unitOfWork.Product.GetAll().ToList();
            return View(categories);
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
                if (obj.Products.Id==0)
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
    }



}

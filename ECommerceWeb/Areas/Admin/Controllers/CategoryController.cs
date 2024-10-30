
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Data.Repository.IRepository;

namespace ECommerceWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public IActionResult Index()
        {
            List<Category> categories = unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Category());
            }
            else
            {
                Category category = unitOfWork.Category.Get(u => u.Id == id)!;
                if (category != null)
                {
                    return View(category);
                }
                else
                    return NotFound();
            }
           
        }

        [HttpPost]
        public IActionResult Upsert(Category obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    unitOfWork.Category.Add(obj);
                    unitOfWork.Save();
                    TempData["success"] = "Category Created Successfully";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    unitOfWork.Category.Update(obj);
                    unitOfWork.Save();
                    TempData["success"] = "Category Updated Successfully";
                    return RedirectToAction("Index", "Category");
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
            Category category = unitOfWork.Category.Get(u => u.Id == id)!;
            if (category != null)
            {
                return View(category);
            }
            else
                return NotFound();
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? c = unitOfWork.Category.Get(u => u.Id == id);
            if (c == null)
            {
                return NotFound();
            }
            unitOfWork.Category.Remove(c);
            unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index", "Category"); //parameters = action name, controller name

        }
    }


}





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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(obj);
                unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View("Error");
        }


        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Category category = unitOfWork.Category.Get(u => u.Id == id)!;
                if (category != null)
                {
                    return View(category);
                }
                else
                    return NotFound();
            }

            return View("Error");

        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(obj);
                unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");
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




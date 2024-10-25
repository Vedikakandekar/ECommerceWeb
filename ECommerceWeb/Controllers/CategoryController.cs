
using ECommerce.Models;
using ECommerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Category.ToList();
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
                _db.Category.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View("Error");
        }


        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Category category = _db.Category.Find(id)!;
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
                _db.Category.Update(obj);
                _db.SaveChanges();
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
                Category category = _db.Category.Find(id)!;
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
            Category? c = _db.Category.Find(id);
            if (c == null)
            {
                return NotFound();
            }
            _db.Category.Remove(c);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index", "Category"); //parameters = action name, controller name

        }
    }


}




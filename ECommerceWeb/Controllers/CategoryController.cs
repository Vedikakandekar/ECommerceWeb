using ECommerceWeb.Data;
using ECommerceWeb.Models;
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
                return RedirectToAction("Index","Category");//params=action,controller
            }
            return View("Error");
        }

    }
}

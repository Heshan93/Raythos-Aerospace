using Microsoft.AspNetCore.Mvc;
using Raythos_Aerospace.Data;
using Raythos_Aerospace.Models;

namespace Raythos_Aerospace.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AplicationDbContext _db;

        public CategoryController(AplicationDbContext db)
        {
                _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Catagory> objectCategoryList = _db.Catagorys ;

            return View(objectCategoryList);
        }
        //Get
        public IActionResult Create()
        {
     
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Catagory obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "DisplayOrder can not be same as the name");
            }

            if (ModelState.IsValid)
            {
                _db.Catagorys.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}

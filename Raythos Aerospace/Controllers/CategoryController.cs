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
                TempData["success"] = "Category created successfuly";
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        //Get
        public IActionResult Edit(int? Id)
        {
            if(Id==null || Id == 0)
            {
                return NotFound();
            }

            var CategoryFromDb = _db.Catagorys  .Find(Id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Catagory obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "DisplayOrder can not be same as the name");
            }

            if (ModelState.IsValid)
            {
                _db.Catagorys.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edit successfuly";
                return RedirectToAction("Index");
            }

            return View(obj);
        }



        //Get
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var CategoryFromDb = _db.Catagorys.Find(Id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Catagorys.Find(Id); 
            
            if (obj == null)
            {
                return NotFound();
            }

                _db.Catagorys.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category Deleted successfuly";
            return RedirectToAction("Index");
            

            return View(obj); 
        }
    }
}

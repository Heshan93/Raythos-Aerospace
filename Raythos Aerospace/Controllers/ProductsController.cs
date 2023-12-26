using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Raythos_Aerospace.Data;
using Raythos_Aerospace.Models;


namespace Raythos_Aerospace.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostingEnvironment;



        public ProductsController(AplicationDbContext context, IWebHostEnvironment webHostingEnvironment)
        {
            _context = context;
            _webHostingEnvironment = webHostingEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'AplicationDbContext.Products'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product)
        {
           
            ModelState.Remove("ImagePath");


            if (ModelState.IsValid)
            {
                var imageFile = product.ImageFile;

                if (imageFile == null || imageFile.Length == 0)
                {
                    ModelState.AddModelError("ImageFile", "Please select an image");
                    return View(product);
                }

                string folder = "images";
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string serverPath = Path.Combine(_webHostingEnvironment.WebRootPath, folder, fileName);


                // Create the directory if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));

                // Save the file to the specified location
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                product.ImagePath = $"/{folder}/{fileName}";

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }






        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            ModelState.Remove("ImagePath");

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try 
                { 
                    //Image upload


                    var imageFile = product.ImageFile;

                    if (imageFile == null || imageFile.Length == 0)
                    {
                        ModelState.AddModelError("ImageFile", "Please select an image");
                        return View(product);
                    }

                    string folder = "images";
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string serverPath = Path.Combine(_webHostingEnvironment.WebRootPath, folder, fileName);


                    // Create the directory if it doesn't exist
                    Directory.CreateDirectory(Path.GetDirectoryName(serverPath));

                    // Save the file to the specified location
                    using (var stream = new FileStream(serverPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    product.ImagePath = $"/{folder}/{fileName}";

                    //Image upload

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

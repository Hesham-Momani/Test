using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryStore.Data;
using GroceryStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace GroceryStore.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]

    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Products
        public async Task<IActionResult> Index(int id)
        {
            ViewData["CategoryId"] = id;
            List<Product> result;
            if (id==0)
            {
            result = await _context.products.ToListAsync();

            }
            else
            {
                result = await _context.products.Where(x => x.CategoryId == id).ToListAsync();

            }
            return View(result);
        }

        // GET: Dashboard/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Dashboard/Products/Create
        public IActionResult Create(int id)
        {
            ViewData["CategoryId"] = id;
            return View();
        }

        // POST: Dashboard/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,ProductPrice,ProductImg,CategoryId")] Product product, IFormFile ProductImg)
        {
           
                if (ProductImg != null && ProductImg.Length > 0)
                {
                    // Define the path where the image will be saved
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Systemimages", ProductImg.FileName);

                    // Save the image to the folder
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProductImg.CopyToAsync(stream);
                    }

                    // Save the image path to your category object (ensure your model has this property)
                    product.ProductImg = ProductImg.FileName;
                }
                else
                {
                    // If the image is required, return an error if it's missing
                    ModelState.AddModelError("CategoryImage", "The image is required.");
                    return View(product);
                }
                _context.Add(product);
                await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = product.CategoryId });
        }

        // GET: Dashboard/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Dashboard/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductDescription,ProductPrice,ProductImg,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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

        // GET: Dashboard/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Dashboard/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
                _context.products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.products.Any(e => e.ProductId == id);
        }
    }
}

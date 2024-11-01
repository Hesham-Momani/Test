using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryStore.Data;
using GroceryStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace GroceryStore.Areas.Dashboard.Controllers
{
    [Authorize]

    [Area("Dashboard")]
    public class FeatursController : Controller
    {
        private readonly AppDbContext _context;

        public FeatursController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Featurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.features.ToListAsync());
        }

        // GET: Dashboard/Featurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featur = await _context.features
                .FirstOrDefaultAsync(m => m.FeaturId == id);
            if (featur == null)
            {
                return NotFound();
            }

            return View(featur);
        }

        // GET: Dashboard/Featurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Featurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeaturId,FeaturName,FeaturDescription,IsDeleted,IsActive,CreationDate,UpdateDate")] Featur featur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(featur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(featur);
        }

        // GET: Dashboard/Featurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featur = await _context.features.FindAsync(id);
            if (featur == null)
            {
                return NotFound();
            }
            return View(featur);
        }

        // POST: Dashboard/Featurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeaturId,FeaturName,FeaturDescription,IsDeleted,IsActive,CreationDate,UpdateDate")] Featur featur)
        {
            if (id != featur.FeaturId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeaturExists(featur.FeaturId))
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
            return View(featur);
        }

        // GET: Dashboard/Featurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featur = await _context.features
                .FirstOrDefaultAsync(m => m.FeaturId == id);
            if (featur == null)
            {
                return NotFound();
            }

            return View(featur);
        }

        // POST: Dashboard/Featurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var featur = await _context.features.FindAsync(id);
            if (featur != null)
            {
                _context.features.Remove(featur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeaturExists(int id)
        {
            return _context.features.Any(e => e.FeaturId == id);
        }
    }
}

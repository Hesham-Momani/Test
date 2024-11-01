using GroceryStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Areas.Dashboard.Controllers
{
    [Authorize]

    [Area("Dashboard")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            ViewData["contacts"] = _context.contacts.ToList().Count();
            ViewData["features"] = _context.features.ToList().Count();
            ViewData["categories"] = _context.categories.ToList().Count();
            ViewData["products"] = _context.products.ToList().Count();
            return View();
        }
    }
}

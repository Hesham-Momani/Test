using GroceryStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Models.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
 
        private readonly AppDbContext _db;

        public CategoryViewComponent(AppDbContext context)
        {
            _db = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_db.categories.ToList());
        }

    }
}

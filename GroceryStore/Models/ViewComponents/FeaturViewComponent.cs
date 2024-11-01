
using GroceryStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Models.ViewComponents
{
    public class FeaturViewComponent : ViewComponent
    {

        private readonly AppDbContext _db;
        public FeaturViewComponent(AppDbContext context)
        {
            _db = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_db.features.ToList());
        }

    }
}

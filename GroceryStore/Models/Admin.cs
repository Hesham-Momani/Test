using GroceryStore.Models.CommonProp;

namespace GroceryStore.Models
{
    public class Admin: SharedProp
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    }
}

using GroceryStore.Models.CommonProp;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class Order: SharedProp
    {
        public int OrderId { get; set; }
        [Display(Name = "Order Status")]
        public string? OrderStatus { get; set; }
        public string? UserId { get; set; }
    }
}

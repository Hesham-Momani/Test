using GroceryStore.Models.CommonProp;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class Cart: SharedProp
    {
        public int CartId { get; set; }
        public int? ProductId { get; set; }
        [Display(Name = "Product name")]
        public string? Productname	 { get; set; }
        public float? price { get; set; }
        public int? Quantity { get; set; }
        public float? Total { get; set; }
        [Display(Name = "Product Image")]

        public string? ProductImage { get; set; }

        public string? Status { get; set; }
        public int? OrderId { get; set; }

    }
}

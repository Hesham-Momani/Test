using GroceryStore.Models.CommonProp;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class Product: SharedProp
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        [Display(Name = "Product Price")]
        public string ProductPrice { get; set; }
        [Display(Name = "Product Image")]
        public string? ProductImg { get; set; }
        public int CategoryId { get; set; }

    }
}

using GroceryStore.Models.CommonProp;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class Category : SharedProp
    {
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Category Image")]
        public string? CategoryImage { get; set; }
    }
}

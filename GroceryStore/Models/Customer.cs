using GroceryStore.Models.CommonProp;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class Customer: SharedProp
    {
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Gender")]
        public string CustomerGender { get; set; }
        [Display(Name = "Customer Email")]
        public string CustomerEmail { get; set; }
        [Display(Name = "Customer Phone")]
        public string CustomerPhone { get; set; }
        [Display(Name = "Customer Password")]
        public string CustomerPassword { get; set; }
        [Display(Name = "Customer Country")]
        public string CustomerCountry { get; set; }
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Customer City")]
        public string CustomerCity { get; set; }
        [Display(Name = "Customer Image")]
        public string CustomerImg { get; set; }
    }
}

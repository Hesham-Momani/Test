using GroceryStore.Models.CommonProp;
using PayPalCheckoutSdk.Orders;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class Contact: SharedProp
    {
        public int ContactId { get; set; }
        [Display(Name = "Contact Address")]
        public string? StoreAddress { get; set; }
        [Display(Name = "User Name")]
        public string?UserName { get; set; }
        [Display(Name= "Contact Subject")]
        public string?ContactSubject { get; set; }
        [Display(Name = "Contact Message")]
        public string?ContactMessage { get; set; }
    }
}

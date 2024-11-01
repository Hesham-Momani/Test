using GroceryStore.Models.CommonProp;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models
{
    public class Featur : SharedProp
    {
        public int FeaturId { get; set; }
        [Display(Name = "Featur Name")]
        public string FeaturName { get; set; }
        [Display(Name = "Featur Description")]
        public string FeaturDescription { get; set; }
    }
}

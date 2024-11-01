using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter Your User Name")]
        [MinLength(2)]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please Enter your Email")]
        [EmailAddress]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please Enter Your Phone Number With Out +962")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits")]
        [RegularExpression(@"^07[789]\d{7}$", ErrorMessage = "Phone number must be numeric and 10 digits long")]
        public string Mobile { get; set; }
     


    }
}

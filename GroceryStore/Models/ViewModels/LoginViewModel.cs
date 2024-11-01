using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter your Email")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

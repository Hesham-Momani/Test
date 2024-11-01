using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}

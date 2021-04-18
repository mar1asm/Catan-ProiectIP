using System.ComponentModel.DataAnnotations;

namespace CatanAPI.Models.Authentication
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
    }
}
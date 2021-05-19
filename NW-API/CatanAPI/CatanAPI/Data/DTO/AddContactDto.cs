using System.ComponentModel.DataAnnotations;
namespace CatanAPI.Data.DTO
{
    public class AddContactDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
    }
}
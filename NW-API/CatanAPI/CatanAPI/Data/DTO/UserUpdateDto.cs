using System.ComponentModel.DataAnnotations;

namespace CatanAPI.Data.DTO
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

    }
}
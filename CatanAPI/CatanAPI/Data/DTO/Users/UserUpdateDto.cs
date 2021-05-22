using System.ComponentModel.DataAnnotations;

namespace CatanAPI.Data.DTO.UsersDTO
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string IconPath { get; set; }
        public int? Level { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

    }
}
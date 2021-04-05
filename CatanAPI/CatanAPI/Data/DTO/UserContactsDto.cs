using System.Collections.Generic;

namespace CatanAPI.Models
{
    public class UserContactsDto
    {
        public int UserId { get; set; }

        public List<ContactDto> Contacts { get; set; }

    }
}
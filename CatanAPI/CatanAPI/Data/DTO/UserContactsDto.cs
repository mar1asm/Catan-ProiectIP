using System.Collections.Generic;

namespace CatanAPI.Data.DTO
{
    public class UserContactsDto
    {
        public int UserId { get; set; }

        public List<ContactDto> Contacts { get; set; }

    }
}
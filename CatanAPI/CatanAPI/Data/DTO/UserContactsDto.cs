using System.Collections.Generic;

namespace CatanAPI.Data.DTO
{
    public class UserContactsDto
    {
        public string UserId { get; set; }

        public List<ContactDto> Contacts { get; set; }

    }
}
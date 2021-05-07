using System.Collections.Generic;

namespace CatanAPI.Data.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<NotificationDto> Notifications { get; set; }
        public List<ContactDto> Contacts { get; set; }

    }
}
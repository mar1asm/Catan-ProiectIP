using CatanAPI.Data.DTO.NotificationsDTO;
using System.Collections.Generic;

namespace CatanAPI.Data.DTO.UsersDTO
{
    public class GetUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IconPath { get; set; }
        public int Level { get; set; }
        public Models.UserRoles Role { get; set; } 
        public int NoOfGames { get; set; }
        public int NoOfWonGames { get; set; }
        public int TimeOnPlay { get; set; }

        public List<NotificationDto> Notifications { get; set; }
        public List<ContactDto> Contacts { get; set; }

    }
}
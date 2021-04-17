using System.Collections.Generic;
using System;
namespace CatanAPI.Models
{
    [Flags]
    public enum UserRoles
    {
        User          = 0x001,
        Administrator = 0x010
    }
    public class User : Microsoft.AspNetCore.Identity.IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Roles { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        public List<UserNotification> UserNotifications { get; set; }

        public ICollection<GameSession> GameSessions { get; set; }
        public List<GameSessionUser> GameSessionUsers { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}

using System.Collections.Generic;

namespace CatanAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public short Roles { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        public ICollection<GameSession> GameSessions { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
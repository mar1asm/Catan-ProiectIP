using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;

namespace CatanAPI.Models
{
    [Flags]
    public enum UserRoles
    {
        User          = 0x001,
        Administrator = 0x010
    }
    public class User : IdentityUser
    {
 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRoles Roles { get; set; }
        public string IconPath { get; set; }
        public int Level { get; set; }
        public int NoOfGames { get; set; }
        public int NoOfWonGames { get; set; }
        public int TimeOnPlay { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        public List<UserNotification> UserNotifications { get; set; }

        public ICollection<GameSession> GameSessions { get; set; }
        public List<GameSessionUser> GameSessionUsers { get; set; }

        public virtual ICollection<Contact> SentContactRequests { get; set; }

        public virtual ICollection<Contact> ReceievedContactRequests { get; set; }
        [NotMapped]
        public virtual ICollection<Contact> Contacts
        {
            get
            {
                var contacts = SentContactRequests.Where(x => x.Accepted).ToList();
                contacts.AddRange(ReceievedContactRequests.Where(x => x.Accepted));
                return contacts.ToList();
            }
        }

    }
}
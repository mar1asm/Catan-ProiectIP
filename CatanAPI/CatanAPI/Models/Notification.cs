
using System.Collections.Generic;
using System;
namespace CatanAPI.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<User> Users { get; set; }
        public List<UserNotification> UserNotifications { get; set; }
    }
}
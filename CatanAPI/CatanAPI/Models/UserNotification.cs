using System.Collections.Generic;
using System;
namespace CatanAPI.Models
{
    public class UserNotification
    {
        public int Id;
        public DateTime CreatedAt { get; set; }
        public bool Read { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}
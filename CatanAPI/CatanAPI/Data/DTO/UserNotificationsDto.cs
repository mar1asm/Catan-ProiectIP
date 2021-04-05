using System.Collections.Generic;

namespace CatanAPI.Models
{
    public class UserNotificationsDto
    {
        public int UserId { get; set; }

        public List<NotificationDto> Notifications { get; set; }

    }
}
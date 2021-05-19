using System.Collections.Generic;

namespace CatanAPI.Data.DTO.NotificationsDTO
{
    public class UserNotificationsDto
    {
        public int UserId { get; set; }

        public List<NotificationDto> Notifications { get; set; }

    }
}
using System;
namespace CatanAPI.Data.DTO.NotificationsDTO
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Read { get; set; }

    }
}
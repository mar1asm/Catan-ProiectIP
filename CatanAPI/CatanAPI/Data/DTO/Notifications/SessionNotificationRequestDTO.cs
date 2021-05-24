using System;
namespace CatanAPI.Data.DTO.NotificationsDTO
{
    public class SessionNotificationRequestDTO
    {
        public int GameSessionId { get; set; }
        public string Text { get; set; }

    }
}
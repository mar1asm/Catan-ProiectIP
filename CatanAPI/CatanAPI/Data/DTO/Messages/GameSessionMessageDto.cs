using System;

namespace CatanAPI.Data.DTO.Messages
{
    public class GameSessionMessageDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string FromUserName { get; set; }
    }
}

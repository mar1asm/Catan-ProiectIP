using System;

namespace CatanAPI.Data.DTO.Messages
{
    public class PrivateMessageDto
    {
        public int Id { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}

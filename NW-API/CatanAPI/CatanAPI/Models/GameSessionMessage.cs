using System;
using System.Text.Json.Serialization;

namespace CatanAPI.Models
{
    public class GameSessionMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string FromId { get; set; }
        public User From { get; set; }
        public int GameSessionId { get; set; }
        public GameSession GameSession { get; set; }
    }
}

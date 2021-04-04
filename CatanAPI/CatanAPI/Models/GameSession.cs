
using System.Collections.Generic;
using System;
namespace CatanAPI.Models
{
    public class GameSession
    {
        public int GameSessionId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Extension> Extensions { get; set; }
        public ICollection<GameSessionUser> GameSessionUsers { get; set; }
    }
}
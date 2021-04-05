
using System.Collections.Generic;
using System;
namespace CatanAPI.Models
{
    public enum GameSessionStatus
    {
        Pending,
        Started,
        Cancelled,
        Finished
    }
    public class GameSession
    {
        public int GameSessionId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Extension> Extensions { get; set; }

        public ICollection<User> Users { get; set; }
        public List<GameSessionUser> GameSessionUsers { get; set; }
    }
}
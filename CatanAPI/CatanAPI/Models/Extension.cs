using System.Collections.Generic;
using System;
namespace CatanAPI.Models
{
    public class Extension
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }

        public virtual ICollection<GameSession> GameSessions { get; set; }
    }
}

using System.Collections.Generic;
using System;
namespace CatanAPI.Models
{
    public class GameSessionUser
    {
        public int GameSessionUserId { get; set; }
        public int SessionRoles { get; set; }
        public int Status { get; set; }
        

        public int UserId { get; set; }
        public User User { get; set; }

        public int GameSessionId { get; set; }
        public GameSession GameSession { get; set; }
    }
}
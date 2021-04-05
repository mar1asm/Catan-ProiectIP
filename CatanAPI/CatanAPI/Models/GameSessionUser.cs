
using System.Collections.Generic;
using System;
namespace CatanAPI.Models
{
    [Flags]
    public enum GameSessionRoles
    {
        GameUser      = 0x001,
        GameModerator = 0x010,
        GameAdmin     = 0x100
    }
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
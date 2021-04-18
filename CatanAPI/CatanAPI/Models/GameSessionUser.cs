
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
    public enum GameSessionUserStatus
    {
        Pending,
        Accepted
    }
    public class GameSessionUser
    {
        public int SessionRoles { get; set; }
        public int Status { get; set; }
        

        public string UserId { get; set; }
        public User User { get; set; }

        public int GameSessionId { get; set; }
        public GameSession GameSession { get; set; }
    }
}
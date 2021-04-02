using System;

namespace CatanAPI.Models
{
    public class Session
    {
        public int SessionId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
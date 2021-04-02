using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CatanAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public short Roles { get; set; }
        public int SessionId { get; set; }
    }
}
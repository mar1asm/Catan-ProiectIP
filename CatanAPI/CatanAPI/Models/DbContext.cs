using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CatanAPI.Models
{
    public class CatanAPIDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CatanAPI.Models
{
    public class CatanAPIDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Extension> Extensions { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
        

        public CatanAPIDbContext(DbContextOptions<CatanAPIDbContext> options) : base(options) { }
    }
}
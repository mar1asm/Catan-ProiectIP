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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(user => user.Notifications)
                .WithMany(notification => notification.Users)
                .UsingEntity<UserNotification>(
                    userNotification => userNotification
                                        .HasOne(entry => entry.Notification)
                                        .WithMany(entry => entry.UserNotifications)
                                        .HasForeignKey(entry => entry.NotificationId),
                    userNotification => userNotification
                                       .HasOne(entry => entry.User)
                                       .WithMany(user => user.UserNotifications)
                                       .HasForeignKey(entry => entry.UserId),
                    userNotificationBuilder =>
                    {
                        userNotificationBuilder.Property(userNotification => userNotification.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                        userNotificationBuilder.Property(userNotification => userNotification.Read).HasDefaultValue(false);
                        userNotificationBuilder.HasKey(userNotification => new { userNotification.UserId, userNotification.NotificationId });
                    }
                );

            modelBuilder.Entity<GameSession>()
                 .HasMany(gameSession => gameSession.Users)
                 .WithMany(user => user.GameSessions)
                 .UsingEntity<GameSessionUser>(
                     gameSessionUser => gameSessionUser
                                       .HasOne(entry => entry.User)
                                       .WithMany(user => user.GameSessionUsers),
                     gameSessionUser => gameSessionUser
                                       .HasOne(entry => entry.GameSession)
                                       .WithMany(entry => entry.GameSessionUsers),
                     gameSessionUserBuilder =>
                     {
                         gameSessionUserBuilder.Property(gameSessionUser => gameSessionUser.Status).HasDefaultValue(GameSessionStatus.Pending);
                         gameSessionUserBuilder.Property(gameSessionUser => gameSessionUser.SessionRoles).HasDefaultValue(GameSessionRoles.GameUser);
                         gameSessionUserBuilder.HasKey(gameSessionUser => new { gameSessionUser.UserId, gameSessionUser.GameSessionId });
                     }
                );
                
        }
    }
}
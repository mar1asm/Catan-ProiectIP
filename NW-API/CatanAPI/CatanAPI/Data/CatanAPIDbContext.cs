using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CatanAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace CatanAPI.Data
{
    public class CatanAPIDbContext : IdentityDbContext<User>
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public DbSet<Extension> Extensions { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<GameSessionUser> GameSessionUsers { get; set; }

        public DbSet<PrivateMessage> PrivateMessages { get; set; }


        public CatanAPIDbContext(DbContextOptions<CatanAPIDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                         gameSessionUserBuilder.Property(gameSessionUser => gameSessionUser.Status).HasDefaultValue(GameSessionUserStatus.Pending);
                         gameSessionUserBuilder.Property(gameSessionUser => gameSessionUser.SessionRoles).HasDefaultValue(GameSessionRoles.GameUser);
                         gameSessionUserBuilder.HasKey(gameSessionUser => new { gameSessionUser.UserId, gameSessionUser.GameSessionId });
                     })
                ;
            modelBuilder.Entity<GameSessionUser>();
            modelBuilder.Entity<Contact>()
                .HasOne(contact => contact.Sender)
                .WithMany(user => user.SentContactRequests)
                .HasForeignKey(contact => contact.SenderId);
            modelBuilder.Entity<Contact>()
                .HasOne(contact => contact.Receiver)
                .WithMany(user => user.ReceievedContactRequests)
                .HasForeignKey(contact => contact.ReceiverId);
            modelBuilder.Entity<Notification>();
            modelBuilder.Entity<UserNotification>();
            modelBuilder.Entity<Extension>();
            modelBuilder.Entity<PrivateMessage>();
            modelBuilder.Entity<GameSessionMessage>();
        }
    }
}
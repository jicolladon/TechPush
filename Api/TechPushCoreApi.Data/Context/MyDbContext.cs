using Microsoft.EntityFrameworkCore;
using TechPushCoreApi.Data.DbModels;

namespace TechPushCoreApi.Data.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<PushRegistration> PushRegistration { get; set; }

        public virtual DbSet<PushMessage> Message { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserEmail).IsUnique();
            });
            modelBuilder.Entity<PushRegistration>(entity =>
            {
                entity.HasIndex(e => e.DeviceId).IsUnique();
            });
        }
    }
}

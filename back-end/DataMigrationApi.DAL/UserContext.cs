using DataMigrationApi.Core.Entities.SQL_Entities;
using Microsoft.EntityFrameworkCore;

namespace DataMigrationApi.DAL
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-53USJLL;Initial Catalog=User;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasIndex(u => u.Identity)
                    .IsUnique()
                    .IsClustered();

                user.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                user.Property(u => u.LastName)
                    .HasMaxLength(50);

                user.Property(u => u.Age)
                    .HasComputedColumnSql("DATEDIFF(yy, BirthDate, GETDATE()) - CASE WHEN" +
                                         "(MONTH(BirthDate) > MONTH(GETDATE())) OR(MONTH(BirthDate) = MONTH(GETDATE()) " +
                                         "AND DAY(BirthDate) > DAY(GETDATE())) THEN 1 ELSE 0 END");

                user.HasMany(u => u.Emails)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Email>(email =>
            {
                email.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);

                email.Property(e => e.IsConfirmed)
                    .HasDefaultValue(false);

                email.Property(e => e.UserID)
                    .IsRequired();
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}

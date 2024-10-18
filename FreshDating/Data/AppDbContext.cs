using FreshDating.Model;
using Microsoft.EntityFrameworkCore;

namespace FreshDating.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<ProfileLike> ProfileLikes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Match entity configuration
            modelBuilder.Entity<Match>()
                .HasKey(m => m.MatchId); // Use MatchId as the primary key

            // Configure Profile1 relationship
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Profile1)
                .WithMany(p => p.MatchesAsProfile1)
                .HasForeignKey(m => m.Profile1Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Profile2 relationship
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Profile2)
                .WithMany(p => p.MatchesAsProfile2)
                .HasForeignKey(m => m.Profile2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ProfileLike Entity Configuration
            modelBuilder.Entity<ProfileLike>()
                .HasKey(pl => new { pl.FromProfileId, pl.ToProfileId });

            modelBuilder.Entity<ProfileLike>()
                .HasOne(pl => pl.FromProfile)
                .WithMany(p => p.LikesGiven)
                .HasForeignKey(pl => pl.FromProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfileLike>()
                .HasOne(pl => pl.ToProfile)
                .WithMany(p => p.LikesReceived)
                .HasForeignKey(pl => pl.ToProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            // Profile entity relationships
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Gender)
                .WithMany()
                .HasForeignKey(p => p.GenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.City)
                .WithMany()
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraints
            modelBuilder.Entity<Gender>()
                .HasIndex(g => g.GenderName)
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasIndex(c => c.CityName)
                .IsUnique();

            // Seed data for Gender
            modelBuilder.Entity<Gender>().HasData(
                new Gender { GenderId = 1, GenderName = "Male" },
                new Gender { GenderId = 2, GenderName = "Female" },
                new Gender { GenderId = 3, GenderName = "Other" }
            );

            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, CityName = "Copenhagen" },
                new City { CityId = 2, CityName = "Aarhus" },
                new City { CityId = 3, CityName = "Odense" },
                new City { CityId = 4, CityName = "Aalborg" }
                
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

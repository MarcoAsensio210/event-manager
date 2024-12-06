using EventManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the relationships and constraints
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Venue)
                .WithMany()
                .HasForeignKey(e => e.VenueID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

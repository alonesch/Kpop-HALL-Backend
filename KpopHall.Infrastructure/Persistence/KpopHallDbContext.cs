using KpopHall.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KpopHall.Infrastructure.Persistence
{
    public class KpopHallDbContext : DbContext
    {
        public KpopHallDbContext(DbContextOptions<KpopHallDbContext> options) : base(options)
        {
        }

        public DbSet<Artist> Artists => Set<Artist>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(a => a.Name)
                    .IsUnique();
            });
        }
    }
}

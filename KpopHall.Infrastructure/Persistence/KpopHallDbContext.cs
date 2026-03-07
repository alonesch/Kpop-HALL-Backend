using KpopHall.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KpopHall.Infrastructure.Persistence;

public class KpopHallDbContext : DbContext
{
    public KpopHallDbContext(DbContextOptions<KpopHallDbContext> options) : base(options)
    {
    }

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Photocard> Photocards => Set<Photocard>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Member> Members => Set<Member>();

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

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(a => a.Year)
                .IsRequired();

            entity.HasIndex(a => new { a.Title, a.ArtistId })
                .IsUnique();

            entity.HasOne<Artist>()
                .WithMany()
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Photocard>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Version)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(p => new { p.Version, p.AlbumId })
                .IsUnique();

            entity.HasOne<Album>()
                .WithMany()
                .HasForeignKey(p => p.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.OwnsOne(p => p.DistributionContext, dc =>
            {
                dc.Property(d => d.Store)
                    .HasMaxLength(150);

                dc.Property(d => d.Region)
                    .HasMaxLength(100);

                dc.Property(d => d.Event)
                    .HasMaxLength(150);
            });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
           
            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            entity.HasIndex(u => u.Username)
                .IsUnique();
            
            entity.HasIndex(u => u.Email)
                .IsUnique();
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(m => new { m.Name, m.ArtistId })
                .IsUnique();

            entity.HasOne<Artist>()
                .WithMany()
                .HasForeignKey(m => m.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

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
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Artists
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(a => a.Name)
                .IsUnique();
        });
        
        //Albums
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

            entity.HasOne(a => a.Artist)
                .WithMany()
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        //Photocards
        modelBuilder.Entity<Photocard>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .ValueGeneratedNever();

            entity.Property(p => p.Version)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.ArtistId)
            .IsRequired();

            entity.HasIndex(p => p.ArtistId);

            entity.HasIndex(p => new { p.Version, p.AlbumId, p.MemberId })
                .IsUnique();

            entity.Property(p => p.FrontsideImagePath)
                .HasMaxLength(500);

            entity.Property(p => p.BacksideImagePath)
                .HasMaxLength(500);

            entity.HasOne(p => p.Album)
                .WithMany()
                .HasForeignKey(p => p.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Member)
                .WithMany()
                .HasForeignKey(p => p.MemberId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.Artist)
                .WithMany()
                .HasForeignKey(p => p.ArtistId)
                .OnDelete(DeleteBehavior.NoAction);

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
        //Users
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
        //Members
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
        //RefreshTokens
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(rt => rt.Id);
            entity.Property(rt => rt.Token)
                .IsRequired();
            entity.HasIndex(rt => rt.Token)
                .IsUnique();
            entity.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure;

public class StreamingDbContext : DbContext
{
    public StreamingDbContext(DbContextOptions<StreamingDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<UserEntity> Users { get; set; }
    
    public DbSet<AlbumEntity> Albums { get; set; }
    
    public DbSet<ArtistEntity> Artists { get; set; }
    
    public DbSet<SongEntity> Songs { get; set; }
    
    public DbSet<FavoriteEntity> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteEntity>(favorite =>
        {
            favorite.HasOne(f => f.UserEntity)
                .WithMany(f => f.FavoritesEntities)
                .HasForeignKey(f => f.UserId);

            favorite.HasOne(f => f.SongEntity)
                .WithMany(f => f.FavoritesEntities)
                .HasForeignKey(f => f.SongId);
        });

        modelBuilder.Entity<SongEntity>(song =>
        {
            song.HasOne(s => s.ArtistEntity)
                .WithMany(s => s.SongEntities)
                .HasForeignKey(s => s.ArtistId);
            
            song.HasOne(s => s.AlbumEntity)
                .WithMany(s => s.SongEntities)
                .HasForeignKey(s => s.AlbumId);
        });

        modelBuilder.Entity<ArtistEntity>(artist =>
        {
            artist.HasMany(s => s.SongEntities)
                .WithOne(s => s.ArtistEntity)
                .HasForeignKey(s => s.ArtistId);

            artist.HasMany(s => s.AlbumEntities)
                .WithOne(s => s.ArtistEntity)
                .HasForeignKey(s => s.ArtistId);
        });

        modelBuilder.Entity<UserEntity>(user =>
        {
            user.HasMany(s => s.FavoritesEntities)
                .WithOne(f => f.UserEntity)
                .HasForeignKey(f => f.UserId);
        });
    }
}
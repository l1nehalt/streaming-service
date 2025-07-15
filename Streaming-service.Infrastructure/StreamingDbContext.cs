using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Models;

namespace Streaming_service.Infrastructure;

public class StreamingDbContext : DbContext
{
    public StreamingDbContext(DbContextOptions<StreamingDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Album> Albums { get; set; }
    
    public DbSet<Artist> Artists { get; set; }
    
    public DbSet<Song> Songs { get; set; }
    
    public DbSet<Favorite> Favorites { get; set; }
    
    public DbSet<SongFeaturingArtist> SongFeaturingArtists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Favorite>(favorite =>
        {
            favorite.HasOne(f => f.User)
                .WithMany(f => f.Favorites)
                .HasForeignKey(f => f.UserId);

            favorite.HasOne(f => f.Song)
                .WithMany(f => f.Favorites)
                .HasForeignKey(f => f.SongId);
        });

        modelBuilder.Entity<Song>(song =>
        {
            song.HasOne(s => s.Artist)
                .WithMany(s => s.Songs)
                .HasForeignKey(s => s.ArtistId);
            
            song.HasOne(s => s.Album)
                .WithMany(s => s.Songs)
                .HasForeignKey(s => s.AlbumId);
        });

        modelBuilder.Entity<Artist>(artist =>
        {
            artist.HasMany(s => s.Songs)
                .WithOne(s => s.Artist)
                .HasForeignKey(s => s.ArtistId);

            artist.HasMany(s => s.Albums)
                .WithOne(s => s.Artist)
                .HasForeignKey(s => s.ArtistId);
        });

        modelBuilder.Entity<User>(user =>
        {
            user.HasMany(s => s.Favorites)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);
        });
        
        modelBuilder.Entity<SongFeaturingArtist>()
            .HasKey(sfa => new { sfa.SongId, sfa.ArtistId });

        modelBuilder.Entity<SongFeaturingArtist>(song =>
        {
            song.HasOne(f => f.Song)
                .WithMany(f => f.FeaturingArtists)
                .HasForeignKey(f => f.SongId);
        });

        modelBuilder.Entity<SongFeaturingArtist>(artist =>
        {
            artist.HasOne(f => f.Artist)
                .WithMany(f => f.FeaturedInSongs)
                .HasForeignKey(f => f.ArtistId);
        });
    }
}
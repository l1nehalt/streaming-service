using Microsoft.EntityFrameworkCore;
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
}
using Microsoft.EntityFrameworkCore;
using Streaming_service.Infrastructure.Entites;

namespace Streaming_service.Infrastructure.Data;

public class StreamingDbContext : DbContext
{
    public StreamingDbContext(DbContextOptions<StreamingDbContext> options) : base(options)
    {
    }
    
    DbSet<User> Users { get; set; }
    
    DbSet<Album> Albums { get; set; }
    
    DbSet<Artist> Artists { get; set; }
    
    DbSet<Song> Songs { get; set; }
    
    DbSet<Favorite> Favorites { get; set; }
}
namespace Streaming_service.Domain.Models;

public class FavoriteAlbum
{
    public long Id { get; set; }
    
    public long UserId { get; set; } 
    
    public long? AlbumId { get; set; }
    
    public User? User { get; set; }
    
    public Album? Album { get; set; }
}
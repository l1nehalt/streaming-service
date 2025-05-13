namespace Streaming_service.Domain.Models;

public class Favorite
{
    
    public long Id { get; set; }
    
    public long UserId { get; set; }
    
    public long SongId { get; set; }
    
    public Song? Song { get; set; }
    
}
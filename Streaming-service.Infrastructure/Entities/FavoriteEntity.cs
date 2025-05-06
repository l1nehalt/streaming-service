namespace Streaming_service.Infrastructure.Entities;

public class FavoriteEntity
{
    public long Id { get; set; }
    
    public long UserId { get; set; }
    
    public long SongId { get; set; }
    
    public SongEntity SongEntity { get; set; } = new SongEntity();
}
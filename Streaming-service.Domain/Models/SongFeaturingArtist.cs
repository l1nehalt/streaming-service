namespace Streaming_service.Domain.Models;

public class SongFeaturingArtist
{
    public long SongId { get; set; }
    
    public long ArtistId { get; set; }
    
    public Song Song { get; set; } = null!;
    
    public Artist Artist { get; set; } = null!;
}
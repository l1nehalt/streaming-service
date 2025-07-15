namespace Streaming_service.Application.DTOs;

public class FavoriteResponse
{
    public long Id { get; set; }
    public string SongTitle { get; set; } = string.Empty;
    
    public string AlbumTitle { get; set; } = string.Empty;
}
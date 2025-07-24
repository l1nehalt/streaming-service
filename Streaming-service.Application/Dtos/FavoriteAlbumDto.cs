namespace Streaming_service.Application.DTOs;

public class FavoriteAlbumDto
{
    public long Id { get; set; } 
    
    public string Title { get; set; } = string.Empty;
    
    public long ArtistId { get; set; }
    
    public string ImagePath { get; set; } = string.Empty;
    
    public string ArtistName { get; set; } = string.Empty;

    public List<SongDto> Songs { get; set; } = null!;
    
}
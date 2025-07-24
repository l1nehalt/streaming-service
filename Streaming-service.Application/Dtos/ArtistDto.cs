namespace Streaming_service.Application.DTOs;

public class ArtistDto
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;

    public List<SongDto> Songs { get; set; } = [];
    
    public List<AlbumDto> Albums { get; set; } = [];
}
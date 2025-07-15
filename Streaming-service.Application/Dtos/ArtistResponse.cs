namespace Streaming_service.Application.DTOs;

public class ArtistResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;

    public List<SongResponse> Songs { get; set; } = [];
    
    public List<AlbumResponse> Albums { get; set; } = [];
}
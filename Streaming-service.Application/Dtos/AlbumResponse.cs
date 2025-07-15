namespace Streaming_service.Application.DTOs;

public class AlbumResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public long ArtistId { get; set; }

    public string ArtistName { get; set; } = string.Empty;

    public List<SongResponse> Songs { get; set; } = [];

    public string ImagePath { get; set; } = string.Empty;
    
    public string Error  { get; set; } = string.Empty;

}
namespace Streaming_service.Application.DTOs;

public class AlbumResponse
{
    public string Title { get; set; } = string.Empty;

    public string ArtistName { get; set; } = string.Empty;

    public List<SongResponse> Songs { get; set; } = [];

    public string ImagePath { get; set; } = string.Empty;
}
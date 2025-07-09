using Streaming_service.Domain.Models;

namespace Streaming_service.Application.DTOs;

public class SongResponse
{
    public string Title { get; set; } = string.Empty;
    
    public string ArtistName { get; set; } = string.Empty;
    
    public string AlbumTitle { get; set; } = string.Empty;
    
    public string FeaturingArtist { get; set; } = string.Empty;
    
    public string FilePath { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
}
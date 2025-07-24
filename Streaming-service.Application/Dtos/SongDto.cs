using Streaming_service.Domain.Models;

namespace Streaming_service.Application.DTOs;

public class SongDto
{
    public long Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public long ArtistId { get; set; }
    
    public string ArtistName { get; set; } = string.Empty;
    
    public string AlbumTitle { get; set; } = string.Empty;

    public List<ArtistDto> FeaturingArtists { get; set; } = [];
    
    public string FilePath { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
}
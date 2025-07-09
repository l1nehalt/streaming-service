using Streaming_service.Domain.Models;

namespace Streaming_service.Application.DTOs;

public class ArtistsResponse
{
    public string ArtistName { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
}
namespace Streaming_service.Domain.Models;

public class Artist
{
    public long Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;

    public List<Song>? Songs { get; set; } = [];

    public List<Album>? Albums { get; set; } = [];
}
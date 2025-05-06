namespace Streaming_service.Domain.Models;

public class Artist
{
    public long Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
    public List<Song> Singles { get; set; } = new List<Song>();

    public List<Album> Albums { get; set; } = new List<Album>();
}
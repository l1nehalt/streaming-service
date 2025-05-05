namespace Streaming_service.Infrastructure.Entites;

public class Artist
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
    public List<Song> Singles { get; set; } = new List<Song>();

    public List<Album> Albums { get; set; } = new List<Album>();
    
    
}
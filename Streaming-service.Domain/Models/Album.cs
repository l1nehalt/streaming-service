namespace Streaming_service.Domain.Models;

public class Album
{
    public long Id { get; set; }
    
    public long ArtistId { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public string ImagePath { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    
    public List<Song>? Songs { get; set; }
    
    public Artist? Artist { get; set; }
}
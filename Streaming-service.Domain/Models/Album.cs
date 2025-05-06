namespace Streaming_service.Domain.Models;

public class Album
{
    public long Id { get; set; }
    
    public long ArtistId { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public string ImagePath { get; set; } = string.Empty;
    
    public List<Song> Songs { get; set; } = new List<Song>();
    
    public string Title { get; set; } = string.Empty;
    
    public Artist Artist { get; set; } = new Artist();
}
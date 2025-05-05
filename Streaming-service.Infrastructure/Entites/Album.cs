namespace Streaming_service.Infrastructure.Entites;

public class Album
{
    public int Id { get; set; }
    
    public int ArtistId { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public string ImagePath { get; set; } = string.Empty;
    
    public List<Song> Songs { get; set; } = new List<Song>();
    
    public string Title { get; set; } = string.Empty;
    
    public Artist Artist { get; set; } = new Artist();
    
    
}
namespace Streaming_service.Infrastructure.Entities;

public class AlbumEntity
{
    public long Id { get; set; }
    
    public long ArtistId { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public string ImagePath { get; set; } = string.Empty;
    
    public List<SongEntity> Songs { get; set; } = new List<SongEntity>();
    
    public string Title { get; set; } = string.Empty;
    
    public ArtistEntity ArtistEntity { get; set; } = new ArtistEntity();
    
    
}
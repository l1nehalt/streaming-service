namespace Streaming_service.Infrastructure.Entities;

public class AlbumEntity
{
    public long Id { get; set; }
    
    public long ArtistId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public DateTime ReleaseDate { get; set; }
    
    public string ImagePath { get; set; } = string.Empty;
    
    public List<SongEntity>? SongEntities { get; set; }
    
    public ArtistEntity? ArtistEntity { get; set; }
    
}
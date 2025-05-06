namespace Streaming_service.Infrastructure.Entities;

public class ArtistEntity
{
    public long Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
    public List<SongEntity> Singles { get; set; } = new List<SongEntity>();

    public List<AlbumEntity> Albums { get; set; } = new List<AlbumEntity>();
    
}
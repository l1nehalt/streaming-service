namespace Streaming_service.Infrastructure.Entities;

public class ArtistEntity
{
    public long Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;

    public List<SongEntity>? SongEntities { get; set; }

    public List<AlbumEntity>? AlbumEntities { get; set; }
    
}
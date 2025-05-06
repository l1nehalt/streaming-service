namespace Streaming_service.Infrastructure.Entities;

public class SongEntity
{
    public long Id { get; set; }
    
    public long AlbumId { get; set; }

    public long ArtistId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string FeaturingArtists { get; set; } = string.Empty;
    
    public string FilePath { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
    public bool IsSingle  { get; set; }
    
    public ArtistEntity ArtistEntity { get; set; }
    
    public AlbumEntity AlbumEntity { get; set; }
}
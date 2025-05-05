namespace Streaming_service.Infrastructure.Entites;

public class Song
{
    public int Id { get; set; }
    
    public int AlbumId { get; set; }

    public int ArtistId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string FeaturingArtists { get; set; } = string.Empty;
    
    public string FilePath { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
    public bool IsSingle  { get; set; }
    
    public Artist Artist { get; set; }
    
    public Album Album { get; set; }
}
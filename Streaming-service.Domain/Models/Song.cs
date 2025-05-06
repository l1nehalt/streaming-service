namespace Streaming_service.Domain.Models;

public class Song
{
    public long Id { get; set; }
    
    public long AlbumId { get; set; }

    public long ArtistId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string FeaturingArtists { get; set; } = string.Empty;
    
    public string FilePath { get; set; } = string.Empty;
    
    public string ImagePath { get; set; } = string.Empty;
    
    public bool IsSingle  { get; set; }
    
    public Artist Artist { get; set; }
    
    public Album Album { get; set; }

    public Song Create(long id, long albumId, long artistId,
        string title, string featuringArtists, string filePath,
        string imagePath, bool isSingle, Artist artist, Album album)
    {
        var song = new Song
        {
            Id = id,
            AlbumId = albumId,
            ArtistId = artistId,
            Title = title,
            FeaturingArtists = featuringArtists,
            FilePath = filePath,
            ImagePath = imagePath,
            IsSingle = isSingle,
            Artist = artist,
            Album = album
        };
        
        return song;
    }
}
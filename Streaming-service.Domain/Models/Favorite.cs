namespace Streaming_service.Domain.Models;

public class Favorite
{
    
    public long Id { get; set; }
    
    public long UserId { get; set; }
    
    public long SongId { get; set; }
    
    public Song? Song { get; set; }
    

    public static Favorite Create(long id, long userId, long songId, Song song)
    {
        var favorite = new Favorite
        {
            Id = id,
            UserId = userId,
            SongId = songId,
            Song = song
        };
        
        return favorite;
    }
}
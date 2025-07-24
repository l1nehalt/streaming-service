using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IFavoriteRepository
{
    Task<List<FavoriteSong>> GetSongs(long userId);
    
    Task<List<FavoriteAlbum>> GetAlbums(long userId);
    
    Task<bool> AddSong(long userId, long songId);
    
    Task<bool> AddAlbum(long userId, long songId);

    Task<bool> DeleteSong(long userId, long songId);

    Task<bool> DeleteAlbum(long userId, long albumId);
}
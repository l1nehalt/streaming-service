using Streaming_service.Application.DTOs;


namespace Streaming_service.Application.Interfaces;

public interface IFavoriteService
{
    Task<List<FavoriteSongDto>> GetFavoriteSongsAsync(long userId);

    Task<List<FavoriteAlbumDto>> GetFavoriteAlbumsAsync(long userId);
    
    Task<bool> AddFavoriteSongAsync(long userId, long songId);

    Task<bool> AddFavoriteAlbumAsync(long userId, long songId);
    
    Task<bool> RemoveFavoriteSongAsync(long userId, long songId);

    Task<bool> RemoveFavoriteAlbumAsync(long userId, long albumId);
}
using Streaming_service.Application.DTOs;


namespace Streaming_service.Application.Interfaces;

public interface IFavoritesService
{
    Task<List<FavoriteResponse>> GetFavoritesAsync(long userId);
    
    Task<bool> AddFavoriteAsync(long userId, long songId);
    
    Task<bool> RemoveFavoriteAsync(long userId, long songId);
}
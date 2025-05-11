using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface IFavoritesService
{
    Task<List<Favorite>> GetFavoritesAsync(long userId);
    
    Task<Favorite?> AddFavoriteAsync(long userId, long songId);
}
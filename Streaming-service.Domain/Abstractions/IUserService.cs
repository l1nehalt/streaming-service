using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IUserService
{
    Task<List<Favorite>> GetFavoritesAsync(long userId);
    Task<Favorite?> AddFavoriteAsync(long userId, long songId);
}
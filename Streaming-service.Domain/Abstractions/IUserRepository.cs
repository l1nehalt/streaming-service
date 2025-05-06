using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IUserRepository
{
    Task<List<Favorite>> GetFavorites(long userId);
    Task<Favorite?> AddFavorite(long userId, long songId);
}
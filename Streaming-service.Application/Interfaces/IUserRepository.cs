using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface IUserRepository
{
    Task<List<Favorite>> GetAllFavorites(long userId);
    Task<Favorite?> AddFavorite(long userId, long songId);
}
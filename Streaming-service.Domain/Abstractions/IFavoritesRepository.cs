using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IFavoritesRepository
{
    Task<List<Favorite>> Get(long userId);
    
    Task<bool> Add(long userId, long songId);

    Task<bool> Delete(long userId, long songId);
}
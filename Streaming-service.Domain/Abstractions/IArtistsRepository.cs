using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IArtistsRepository
{
    Task<List<Artist>> Get();

    Task<Artist?> GetById(int id);
    
}
using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IArtistRepository
{
    Task<List<Artist>> Get();

    Task<Artist?> GetByName(string name);
    
    Task<Artist> Create(Artist artist);

    Task<Artist?> GetById(long id);
    
}
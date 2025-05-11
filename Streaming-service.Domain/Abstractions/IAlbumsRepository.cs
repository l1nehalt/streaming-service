using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface IAlbumsRepository
{
    Task<List<Album>> Get();
    
    Task<Album?> Search(string query);
}
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Interfaces;

public interface ISongsService
{
    Task<List<Song>> GetSongsAsync();
    
    Task<List<Album>> GetAlbumsAsync();
}
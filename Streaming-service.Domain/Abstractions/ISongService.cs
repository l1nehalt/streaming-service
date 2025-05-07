using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface ISongService
{
    Task<List<Song>> GetSongsAsync();
}
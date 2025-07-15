using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface ISongsRepository
{
    Task<List<Song>> Get();

    Task<List<Song>> GetByArtist(int id);
}
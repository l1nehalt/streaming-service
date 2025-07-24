using Streaming_service.Domain.Models;

namespace Streaming_service.Domain.Abstractions;

public interface ISongRepository
{
    Task<List<Song>> Get();

    Task<Song> Create(Song song);
}
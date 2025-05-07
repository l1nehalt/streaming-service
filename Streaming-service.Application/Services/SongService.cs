using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class SongService : ISongService
{
    private readonly ISongRepository _songRepository;

    public SongService(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<List<Song>> GetSongsAsync()
    {
        return await _songRepository.GetSongs();
    }
}
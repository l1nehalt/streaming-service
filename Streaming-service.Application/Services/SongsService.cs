using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class SongsService : ISongsService
{
    private readonly ISongsRepository _songsRepository;
    private readonly IAlbumsRepository _albumsRepository;

    public SongsService(ISongsRepository songsRepository, IAlbumsRepository albumsRepository)
    {
        _songsRepository = songsRepository;
        _albumsRepository = albumsRepository;
    }

    public async Task<List<Song>> GetSongsAsync()
    {
        return await _songsRepository.Get();
    }

    public async Task<List<Album>> GetAlbumsAsync()
    {
        return await _albumsRepository.Get();
    }
}
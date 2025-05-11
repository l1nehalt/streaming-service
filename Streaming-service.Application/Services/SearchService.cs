using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class SearchService : ISearchService
{
    private readonly ISongsRepository _songsRepository;
    private readonly IAlbumsRepository _albumsRepository;

    public SearchService(ISongsRepository songsRepository, IAlbumsRepository albumsRepository)
    {
        _songsRepository = songsRepository;
        _albumsRepository = albumsRepository;
    }

    public async Task<SearchDto?> SearchAsync(string query)
    {
        var album = await _albumsRepository.Search(query);

        if (album is not null)
        {
            return new SearchDto
            {
                Type = "Album",
                Data = album
            };
        }
        
        var song = await _songsRepository.Search(query);

        if (song is not null)
        {
            return new SearchDto
            {
                Type = "Song",
                Data = song
            };
        }
        
        return null;
    }
    
}
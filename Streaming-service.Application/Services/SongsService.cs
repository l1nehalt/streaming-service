using Streaming_service.Application.DTOs;
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

    public async Task<List<SongResponse>> GetSongsAsync()
    {
        var songs = await _songsRepository.Get();

        if (songs.Count == 0) return new List<SongResponse>();

        var listSongsResponse = songs.Select(songResponse => new SongResponse
        {
            Title = songResponse.Title,
            ArtistName = songResponse.Artist.Name,
            FeaturingArtist = songResponse.FeaturingArtists,
            AlbumTitle = songResponse.Album.Title,
            FilePath = songResponse.FilePath,
            ImagePath = songResponse.ImagePath,
        }).ToList();
        
        return listSongsResponse;
    }

    public async Task<List<AlbumResponse>> GetAlbumsAsync()
    {
        var albums = await _albumsRepository.Get();

        if (albums.Count == 0) return new List<AlbumResponse>();
        
        var listAlbumsResponse = albums.Select(albumResponse => new AlbumResponse
        {
            AlbumId = albumResponse.Id,
            Title = albumResponse.Title,
            ArtistName = albumResponse.Artist.Name,
            ImagePath = albumResponse.ImagePath,
            Songs = albumResponse.Songs.Select(songResponse => new SongResponse
            {
                Title = songResponse.Title,
                ArtistName = songResponse.Artist.Name,
                AlbumTitle = songResponse.Album.Title,
                FilePath = songResponse.FilePath,
                ImagePath = songResponse.ImagePath
            }).ToList()
        }).ToList();
        
        return listAlbumsResponse;
    }

    public async Task<AlbumResponse> GetAlbumByIdAsync(int albumId)
    {
        var album = await _albumsRepository.GetById(albumId);

        if (album == null) return AlbumResponse.Failure("Album not found");

        var albumResponse  = new AlbumResponse
        {
            AlbumId = album.Id,
            Title = album.Title,
            ArtistName = album.Artist.Name,
            ImagePath = album.ImagePath,
            Songs = album.Songs.Select(songResponse => new SongResponse
            {
                Title = songResponse.Title,
                ArtistName = songResponse.Artist.Name,
                AlbumTitle = songResponse.Album.Title,
                FilePath = songResponse.FilePath,
                ImagePath = songResponse.ImagePath
            }).ToList()
        };
        
        return albumResponse;
    }
}
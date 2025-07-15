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

        var songResponseList = songs.Select(songResponse => new SongResponse
        {
            Id = songResponse.Id,
            ArtistId = songResponse.ArtistId,
            Title = songResponse.Title,
            ArtistName = songResponse.Artist.Name,
            FeaturingArtists = songResponse.FeaturingArtists.Select(fa => new ArtistResponse
            {
                Id = fa.ArtistId,
                Name = fa.Artist.Name
            }).ToList(),
            AlbumTitle = songResponse.Album.Title,
            FilePath = songResponse.FilePath,
            ImagePath = songResponse.ImagePath,
        }).ToList();
        
        return songResponseList;
    }

    public async Task<List<AlbumResponse>> GetAlbumsAsync()
    {
        var albums = await _albumsRepository.Get();

        if (albums.Count == 0) return new List<AlbumResponse>();
        
        var albumResponseList = albums.Select(albumResponse => new AlbumResponse
        {
            Id = albumResponse.Id,
            Title = albumResponse.Title,
            ArtistId = albumResponse.ArtistId,
            ArtistName = albumResponse.Artist.Name,
            ImagePath = albumResponse.ImagePath,
            Songs = albumResponse.Songs.Select(songResponse => new SongResponse
            {
                Id = songResponse.Id,
                Title = songResponse.Title,
                ArtistName = songResponse.Artist.Name,
                AlbumTitle = songResponse.Album.Title,
                FeaturingArtists = songResponse.FeaturingArtists.Select(fa => new ArtistResponse
                {
                    Id = fa.ArtistId,
                    Name = fa.Artist.Name
                }).ToList(),
                FilePath = songResponse.FilePath,
                ImagePath = songResponse.ImagePath
            }).ToList()
        }).ToList();
        
        return albumResponseList;
    }

    public async Task<AlbumResponse?> GetAlbumByIdAsync(int albumId)
    {
        var album = await _albumsRepository.GetById(albumId);

        if (album == null) return null;

        var albumResponse  = new AlbumResponse
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            Title = album.Title,
            ArtistName = album.Artist.Name,
            ImagePath = album.ImagePath,
            Songs = album.Songs.Select(songResponse => new SongResponse
            {
                Id = songResponse.Id,
                ArtistId = songResponse.ArtistId,
                Title = songResponse.Title,
                ArtistName = songResponse.Artist.Name,
                AlbumTitle = songResponse.Album.Title,
                FeaturingArtists = songResponse.FeaturingArtists.Select(fa => new ArtistResponse
                {
                    Id = fa.ArtistId,
                    Name = fa.Artist.Name
                }).ToList(),
                FilePath = songResponse.FilePath,
                ImagePath = songResponse.ImagePath
            }).ToList()
        };
        
        return albumResponse;
    }

    public async Task<List<SongResponse>> GetSongsByArtistIdAsync(int artistId)
    {
        var songs = await _songsRepository.GetByArtist(artistId);
        
        if (songs.Count == 0) return new List<SongResponse>();
        
        var songResponseList = songs.Select(songResponse => new SongResponse
        {
            Id = songResponse.Id,
            ArtistId = songResponse.ArtistId,
            ArtistName = songResponse.Artist.Name,
            AlbumTitle = songResponse.Album.Title,
            FeaturingArtists = songResponse.FeaturingArtists.Select(fa => new ArtistResponse
            {
                Id = fa.ArtistId,
                Name = fa.Artist.Name
            }).ToList(),
            FilePath = songResponse.FilePath,
            ImagePath = songResponse.ImagePath,
        }).ToList();
        
        return songResponseList;
    }

    public async Task<List<AlbumResponse>> GetAlbumsByArtistIdAsync(int artistName)
    {
        var albums = await _albumsRepository.GetByArtist(artistName);
        
        if (albums.Count == 0) return new List<AlbumResponse>();
        
        var albumResponseList = albums.Select(albumResponse => new AlbumResponse
        {
            Id = albumResponse.Id,
            Title = albumResponse.Title,
            ArtistId = albumResponse.ArtistId,
            ArtistName = albumResponse.Artist.Name,
            ImagePath = albumResponse.ImagePath,
            Songs = albumResponse.Songs.Select(songResponse => new SongResponse
            {
                Id = songResponse.Id,
                Title = songResponse.Title,
                ArtistName = songResponse.Artist.Name,
                FeaturingArtists = songResponse.FeaturingArtists.Select(fa => new ArtistResponse
                {
                    Id = fa.ArtistId,
                    Name = fa.Artist.Name
                }).ToList(),
                AlbumTitle = songResponse.Album.Title,
                FilePath = songResponse.FilePath,
                ImagePath = songResponse.ImagePath
            }).ToList()
        }).ToList();
        
        return albumResponseList;
    }
}
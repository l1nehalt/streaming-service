using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class SongService : ISongService
{
    private readonly ISongRepository _songRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SongService(ISongRepository songRepository, 
        IAlbumRepository albumRepository, IArtistRepository artistRepository,
        IWebHostEnvironment webHostEnvironment)
    {
        _songRepository = songRepository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<SongDto>> GetSongsAsync()
    {
        var songs = await _songRepository.Get();

        if (songs.Count == 0) return new List<SongDto>();

        var songResponseList = songs.Select(songResponse => new SongDto
        {
            Id = songResponse.Id,
            ArtistId = songResponse.ArtistId,
            Title = songResponse.Title,
            ArtistName = songResponse.Artist.Name,
            FeaturingArtists = songResponse.FeaturingArtists.Select(fa => new ArtistDto
            {
                Id = fa.ArtistId,
                Name = fa.Artist.Name
            }).ToList(),
            AlbumTitle = songResponse.Album?.Title,
            FilePath = songResponse.FilePath,
            ImagePath = songResponse.ImagePath ?? string.Empty
        }).ToList();
        
        return songResponseList;
    }

    public async Task<List<AlbumDto>> GetAlbumsAsync()
    {
        var albums = await _albumRepository.Get();

        if (albums.Count == 0) return new List<AlbumDto>();
        
        var albumResponseList = albums.Select(albumResponse => new AlbumDto
        {
            Id = albumResponse.Id,
            Title = albumResponse.Title,
            ArtistId = albumResponse.ArtistId,
            ArtistName = albumResponse.Artist.Name,
            ImagePath = albumResponse.ImagePath,
            Songs = albumResponse.Songs.Select(songResponse => new SongDto
            {
                Id = songResponse.Id,
                ArtistId = songResponse.ArtistId,
                Title = songResponse.Title,
                FeaturingArtists = songResponse.FeaturingArtists.Select(fa => new ArtistDto
                {
                    Id = fa.ArtistId,
                    Name = fa.Artist.Name
                }).ToList(),
                FilePath = songResponse.FilePath,
                ImagePath = songResponse.ImagePath ?? string.Empty
            }).ToList()
        }).ToList();
        
        return albumResponseList;
    }

    public async Task<AlbumDto?> GetAlbumByIdAsync(long albumId)
    {
        var album = await _albumRepository.GetById(albumId);

        if (album == null) return null;

        var albumResponse  = new AlbumDto
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            Title = album.Title,
            ArtistName = album.Artist.Name,
            ImagePath = album.ImagePath,
            Songs = album.Songs.Select(songResponse => new SongDto
            {
                Id = songResponse.Id,
                ArtistId = songResponse.ArtistId,
                Title = songResponse.Title,
                FeaturingArtists = songResponse.FeaturingArtists.Select(fa => new ArtistDto
                {
                    Id = fa.ArtistId,
                    Name = fa.Artist.Name
                }).ToList(),
                FilePath = songResponse.FilePath,
                ImagePath = songResponse.ImagePath ?? string.Empty
            }).ToList()
        };
        
        return albumResponse;
    }

    public async Task UploadSongAsync(UploadSongDto uploadSongDto)
    {
        if (uploadSongDto.File == null || uploadSongDto.File.Length == 0)
        {
            throw new FileNotFoundException();
        }
        
        var artist = await _artistRepository.GetByName(uploadSongDto.ArtistName);
        
        if (artist == null)
        {
            artist = await _artistRepository.Create(new Artist { Name = uploadSongDto.ArtistName });
        }

        var fileName = Guid.NewGuid() + ".mp3";
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "songs");
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await uploadSongDto.File.CopyToAsync(stream);
        }
        
        var song = new Song
        {
            ArtistId = artist.Id,
            Title = uploadSongDto.Title,
            FilePath = fileName,
        };

        await _songRepository.Create(song);
    }
}
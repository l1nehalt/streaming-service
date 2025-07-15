using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class ArtistsService : IArtistsService
{
    private readonly IArtistsRepository _artistsRepository;

    public ArtistsService(IArtistsRepository artistsRepository)
    {
        _artistsRepository = artistsRepository;
    }

    public async Task<ArtistResponse?> GetByIdAsync(int id)
    {
        var artist = await _artistsRepository.GetById(id);
        
        if (artist == null) return null;

        return new ArtistResponse
        {
            Id = artist.Id,
            Name = artist.Name,
            ImagePath = artist.ImagePath,
            Songs = artist.Songs.Select(song => new SongResponse
            {
                Id = song.Id,
                ArtistId = song.ArtistId,
                Title = song.Title,
                AlbumTitle = song.Album.Title,
                ArtistName = artist.Name,
                FeaturingArtists = song.FeaturingArtists.Select(fa => new ArtistResponse
                {
                    Id = fa.ArtistId,
                    Name = fa.Artist.Name
                }).ToList(),
                FilePath = song.FilePath,
                ImagePath = song.ImagePath
            }).ToList(),
            Albums = artist.Albums.Select(album => new AlbumResponse
            {
                Id = album.Id,
                Title = album.Title,
                ArtistName = artist.Name,
                ImagePath = album.ImagePath
            }).ToList()
        };
    }
}
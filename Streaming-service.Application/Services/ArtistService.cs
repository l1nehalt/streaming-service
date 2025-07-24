using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class ArtistService : IArtistService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistService(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<ArtistDto?> GetByIdAsync(long id)
    {
        var artist = await _artistRepository.GetById(id);
        
        if (artist == null) return null;

        return new ArtistDto
        {
            Id = artist.Id,
            Name = artist.Name,
            ImagePath = artist.ImagePath,
            Songs = artist.Songs?.Select(song => new SongDto
            {
                Id = song.Id,
                ArtistId = song.ArtistId,
                Title = song.Title,
                AlbumTitle = song.Album?.Title,
                ArtistName = artist.Name,
                FeaturingArtists = song.FeaturingArtists.Select(fa => new ArtistDto
                {
                    Id = fa.ArtistId,
                    Name = fa.Artist.Name
                }).ToList(),
                FilePath = song.FilePath,
                ImagePath = song.ImagePath ?? string.Empty,
            }).ToList(),
            Albums = artist.Albums?.Select(album => new AlbumDto
            {
                Id = album.Id,
                ArtistId = album.ArtistId,
                Title = album.Title,
                ArtistName = artist.Name,
                ImagePath = album.ImagePath
            }).ToList()
        };
    }
}
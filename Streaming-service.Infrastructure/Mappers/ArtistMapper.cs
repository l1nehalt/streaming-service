using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure.Mappers;

public class ArtistMapper
{
    public static Artist ToDomain(ArtistEntity entity)
    {
        return new Artist
        {
            Id = entity.Id,
            Name = entity.Name,
            ImagePath = entity.ImagePath,
            Albums = entity.AlbumEntities.Select(albumEntity => new Album
            {
                Id = albumEntity.ArtistEntity.Id,
                ArtistId = albumEntity.ArtistId,
                ReleaseDate = albumEntity.ReleaseDate,
                ImagePath = albumEntity.ImagePath,
                Title = albumEntity.Title
            }).ToList(),
            Songs = entity.SongEntities.Select(songEntity => new Song
            {
                Id = songEntity.ArtistEntity.Id,
                AlbumId = songEntity.ArtistId,
                ArtistId = songEntity.ArtistId,
                Title = songEntity.Title,
                FeaturingArtists = songEntity.FeaturingArtists,
                FilePath = songEntity.FilePath,
                ImagePath = songEntity.ImagePath,
                IsSingle = songEntity.IsSingle,
            }).ToList()
        };
    }
}
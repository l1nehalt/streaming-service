using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure.Mappers;

public class AlbumMapper
{
    public static Album ToDomain(AlbumEntity entity)
    {
        return new Album
        {
            Id = entity.Id,
            ArtistId = entity.ArtistId,
            ReleaseDate = entity.ReleaseDate,
            ImagePath = entity.ImagePath,
            Title = entity.Title,
            Songs = entity.SongEntities.Select(songEntity => new Song
            {
                Id = songEntity.Id,
                ArtistId = songEntity.ArtistId,
                AlbumId = songEntity.AlbumId,
                Title = songEntity.Title,
                FeaturingArtists = songEntity.FeaturingArtists,
                ImagePath = songEntity.ImagePath,
                IsSingle = songEntity.IsSingle,
                FilePath = songEntity.FilePath
            }).ToList(),
            Artist = new Artist
            {
                Id = entity.ArtistId,
                Name = entity.ArtistEntity.Name,
                ImagePath = entity.ArtistEntity.ImagePath
            }
        };
    }
}
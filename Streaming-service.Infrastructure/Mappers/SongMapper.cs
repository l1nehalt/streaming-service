using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure.Mappers;

public class SongMapper
{
    public static Song ToDomain(SongEntity entity)
    {
        return new Song
        {
            Id = entity.Id,
            AlbumId = entity.AlbumId,
            ArtistId = entity.ArtistId,
            Title = entity.Title,
            FeaturingArtists = entity.FeaturingArtists,
            FilePath = entity.FilePath,
            ImagePath = entity.ImagePath,
            IsSingle = entity.IsSingle,
            Artist = new Artist
            {
                Id = entity.ArtistEntity.Id,
                Name = entity.ArtistEntity.Name,
                ImagePath = entity.ArtistEntity.ImagePath
            },
            Album = new Album
            {
                Id = entity.AlbumEntity.Id,
                ArtistId = entity.AlbumEntity.ArtistId,
                ReleaseDate = entity.AlbumEntity.ReleaseDate,
                ImagePath = entity.AlbumEntity.ImagePath,
                Title = entity.AlbumEntity.Title,
            }
        };
    }
}
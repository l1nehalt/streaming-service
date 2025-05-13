using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure.Mappers;

public class FavoriteMapper
{
    public static Favorite ToDomain(FavoriteEntity entity)
    {
        return new Favorite
        {
            Id = entity.Id,
            UserId = entity.UserId,
            SongId = entity.SongId,
            Song = new Song
            {
                Id = entity.SongEntity.Id,
                AlbumId = entity.SongEntity.AlbumId,
                ArtistId = entity.SongEntity.ArtistId,
                Title = entity.SongEntity.Title,
                FeaturingArtists = entity.SongEntity.FeaturingArtists,
                FilePath = entity.SongEntity.FilePath,
                ImagePath = entity.SongEntity.ImagePath,
                IsSingle = entity.SongEntity.IsSingle,
                Artist = new Artist
                {
                    Id = entity.SongEntity.ArtistEntity.Id,
                    Name = entity.SongEntity.ArtistEntity.Name,
                    ImagePath = entity.SongEntity.ArtistEntity.ImagePath,
                },
                Album = new Album
                {
                    Id = entity.SongEntity.AlbumEntity.Id,
                    ArtistId = entity.SongEntity.AlbumEntity.ArtistId,
                    ReleaseDate = entity.SongEntity.AlbumEntity.ReleaseDate,
                    ImagePath = entity.SongEntity.AlbumEntity.ImagePath,
                    Title = entity.SongEntity.AlbumEntity.Title
                }
            }
        };
    }
}
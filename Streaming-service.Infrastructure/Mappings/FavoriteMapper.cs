using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure.Mappings;

public class FavoriteMapper
{
    public static Favorite ToDomain(FavoriteEntity entity, bool includeRelatedEntities = true)
    {
        var favorite = new Favorite
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
                IsSingle = entity.SongEntity.IsSingle
            }
        };

        if (includeRelatedEntities && entity.SongEntity.AlbumEntity != null)
        {
            favorite.Song.Album = new Album
            {
                Id = entity.SongEntity.AlbumEntity.Id,
                ArtistId = entity.SongEntity.AlbumEntity.ArtistId,
                Title = entity.SongEntity.AlbumEntity.Title,
                ReleaseDate = entity.SongEntity.AlbumEntity.ReleaseDate,
                ImagePath = entity.SongEntity.AlbumEntity.ImagePath,

            };
        }

        if (includeRelatedEntities && entity.SongEntity.ArtistEntity != null)
        {
            favorite.Song.Artist = new Artist
            {
                Id = entity.SongEntity.ArtistEntity.Id,
                ImagePath = entity.SongEntity.ArtistEntity.ImagePath,
                Name = entity.SongEntity.ArtistEntity.Name,
            };
        }
        
        return favorite;
    }
}
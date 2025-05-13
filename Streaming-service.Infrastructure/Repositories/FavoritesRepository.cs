using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Infrastructure.Entities;
using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Mappers;


namespace Streaming_service.Infrastructure.Repositories;

public class FavoritesRepository : IFavoritesRepository
{
    private readonly StreamingDbContext _context;
    
    public FavoritesRepository(StreamingDbContext context) 
    {
        _context = context;
    }
    
    public async Task<List<Favorite>> Get(long userId)
    {
        var favoriteEntities = await _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.SongEntity)
            .ThenInclude(f => f.ArtistEntity)
            .Include(f => f.SongEntity)
            .ThenInclude(f => f.AlbumEntity)
            .AsNoTracking()
            .ToListAsync();

        var favorites = favoriteEntities.Select(FavoriteMapper.ToDomain).ToList();
        
        return favorites;
    }

    public async Task<Favorite?> Add(long userId, long songId)
    {
        var songEntity = await _context.Songs
            .FindAsync(songId);
        
        if (songEntity == null)
        {
            return null;
        }

        var favoriteEntity = new FavoriteEntity
        {
            UserId = userId,
            SongId = songId,
            SongEntity = songEntity
        };
        
         await _context.Favorites.AddAsync(favoriteEntity);
         await _context.SaveChangesAsync();

         var favorite = new Favorite
         {
             Id = favoriteEntity.Id,
             UserId = favoriteEntity.UserId,
             SongId = favoriteEntity.SongId,
             Song = new Song
             {
                 Id = favoriteEntity.SongEntity.Id,
                 AlbumId = favoriteEntity.SongEntity.AlbumId,
                 ArtistId = favoriteEntity.SongEntity.ArtistId,
                 Title = favoriteEntity.SongEntity.Title,
                 FilePath = favoriteEntity.SongEntity.FilePath,
                 FeaturingArtists = favoriteEntity.SongEntity.FeaturingArtists,
                 ImagePath = favoriteEntity.SongEntity.ImagePath,
                 IsSingle = favoriteEntity.SongEntity.IsSingle
             }
         };
         
         return favorite;
    }

    public async Task<Favorite?> Delete(long userId, long songId)
    {
        var favoriteEntity = await _context.Favorites
            .Where(a => a.UserId == userId && a.SongId == songId)
            .Include(f => f.SongEntity)
            .FirstOrDefaultAsync();
        
        if (favoriteEntity == null)
        {
            return null;
        }
        
        _context.Favorites.Remove(favoriteEntity);
        await _context.SaveChangesAsync();

        var favorite = new Favorite
        {
            Id = favoriteEntity.Id,
            SongId = songId,
            UserId = userId,
            Song = new Song
            {
                Id = favoriteEntity.SongEntity.Id,
                AlbumId = favoriteEntity.SongEntity.AlbumId,
                ArtistId = favoriteEntity.SongEntity.ArtistId,
                Title = favoriteEntity.SongEntity.Title,
                FilePath = favoriteEntity.SongEntity.FilePath,
                FeaturingArtists = favoriteEntity.SongEntity.FeaturingArtists,
                ImagePath = favoriteEntity.SongEntity.ImagePath,
                IsSingle = favoriteEntity.SongEntity.IsSingle
            }
        };
        
        return favorite;
    }
}
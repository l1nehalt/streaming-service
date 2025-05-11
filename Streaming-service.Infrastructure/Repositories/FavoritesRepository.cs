using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Infrastructure.Entities;
using Streaming_service.Domain.Models;


namespace Streaming_service.Infrastructure.Repositories;

public class FavoritesRepository : IFavoritesRepository
{
    private readonly StreamingDbContext _context;
    private readonly IMapper _mapper;
    
    public FavoritesRepository(StreamingDbContext context, IMapper mapper) 
    {
        _context = context;
        _mapper = mapper;
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

        var favorites = favoriteEntities.Select(favoriteEntity => new Favorite
        {
            Id = favoriteEntity.Id,
            UserId = favoriteEntity.UserId,
            SongId = favoriteEntity.SongEntity.Id,
            Song = new Song
            {
                Id = favoriteEntity.SongEntity.Id,
                AlbumId = favoriteEntity.SongEntity.AlbumId,
                ArtistId = favoriteEntity.SongEntity.ArtistId,
                Title = favoriteEntity.SongEntity.Title,
                FilePath = favoriteEntity.SongEntity.FilePath,
                FeaturingArtists = favoriteEntity.SongEntity.FeaturingArtists,
                ImagePath = favoriteEntity.SongEntity.ImagePath,
                IsSingle = favoriteEntity.SongEntity.IsSingle,
                Artist = new Artist
                {
                    Id = favoriteEntity.SongEntity.ArtistEntity.Id,
                    Name = favoriteEntity.SongEntity.ArtistEntity.Name,
                    ImagePath = favoriteEntity.SongEntity.ArtistEntity.ImagePath,
                },
                Album = new Album
                {
                    Id = favoriteEntity.SongEntity.AlbumEntity.Id,
                    ArtistId = favoriteEntity.SongEntity.AlbumEntity.ArtistId,
                    ReleaseDate = favoriteEntity.SongEntity.AlbumEntity.ReleaseDate,
                    ImagePath = favoriteEntity.SongEntity.AlbumEntity.ImagePath,
                    Title = favoriteEntity.SongEntity.AlbumEntity.Title,
                }
            }
        }).ToList();
        
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
         
         return _mapper.Map<Favorite>(favoriteEntity);
    }
}
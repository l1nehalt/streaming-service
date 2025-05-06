using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Infrastructure.Entities;
using Streaming_service.Domain.Models;


namespace Streaming_service.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly StreamingDbContext _context;
    private readonly IMapper _mapper;
    
    public UserRepository(StreamingDbContext context, IMapper mapper) 
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<Favorite>> GetFavorites(long userId)
    {
        var favoriteEntities = await _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.SongEntity)
            .ThenInclude(f => f.ArtistEntity)
            .Include(f => f.SongEntity)
            .ThenInclude(f => f.AlbumEntity)
            .AsNoTracking()
            .ToListAsync();
            
        return _mapper.Map<List<Favorite>>(favoriteEntities);
    }

    public async Task<Favorite?> AddFavorite(long userId, long songId)
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
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Streaming_service.Infrastructure.Entities;
using Streaming_service.Application.Interfaces;
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
    
    public async Task<List<Favorite>> GetAllFavorites(long userId)
    {
        var favoriteEntities = await _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.SongEntity)
            .ToListAsync();
            
        return _mapper.Map<List<Favorite>>(favoriteEntities);
    }

    public async Task<Favorite?> AddFavorite(long userId, long songId)
    {
        var songEntity = await _context.Songs.AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == songId);

        if (songEntity == null)
        {
            return null;
        }

        var favoriteEntity = new FavoriteEntity
        {
            UserId = userId,
            SongId = songId
        };
        
         await _context.Favorites.AddAsync(favoriteEntity);
         await _context.SaveChangesAsync();
         
         return _mapper.Map<Favorite>(favoriteEntity);
    }
}
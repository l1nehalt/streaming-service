using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;


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
        var favorites = await _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Song)
            .ThenInclude(f => f.Artist)
            .Include(f => f.Song)
            .ThenInclude(f => f.Album)
            .ToListAsync();
        
        return favorites;
    }

    public async Task<bool> Add(long userId, long songId)
    {
        var song = await _context.Songs.FindAsync(songId);
        var user = await _context.Users.FindAsync(userId);
        
        if (song == null || user == null)
        {
            return false;
        }

        var favorite = new Favorite
        {
            UserId = userId,
            SongId = songId,
            Song = song
        };
        
         await _context.Favorites.AddAsync(favorite);
         await _context.SaveChangesAsync();
         
         return true;
    }

    public async Task<bool> Delete(long userId, long songId)
    {
        var favorite = await _context.Favorites
            .Where(a => a.UserId == userId && a.SongId == songId)
            .Include(f => f.Song)
            .FirstOrDefaultAsync();
        
        if (favorite == null)
        {
            return false;
        }
        
        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
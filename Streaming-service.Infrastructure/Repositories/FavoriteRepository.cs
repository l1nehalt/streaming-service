using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;


namespace Streaming_service.Infrastructure.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly StreamingDbContext _context;
    
    public FavoriteRepository(StreamingDbContext context) 
    {
        _context = context;
    }
    
    public async Task<List<FavoriteSong>> GetSongs(long userId)
    {
        var favorites = await _context.FavoriteSongs
            .Where(f => f.UserId == userId)
            .Include(f => f.Song)
            .ThenInclude(s => s.Artist)
            .Include(f => f.Song)
            .ThenInclude(s => s.Album)
            .Include(f => f.Song)
            .ThenInclude(s => s.FeaturingArtists)
            .ThenInclude(fa => fa.Artist) 
            .ToListAsync();
        
        return favorites;
    }

    public async Task<List<FavoriteAlbum>> GetAlbums(long userId)
    {
        var favorites = await _context.FavoriteAlbums
            .Where(f => f.UserId == userId)
            .Include(f => f.Album)
            .ThenInclude(a => a.Artist)
            .Include(f => f.Album)
            .ThenInclude(a => a.Songs)
            .ThenInclude(s => s.FeaturingArtists)
            .ThenInclude(fa => fa.Artist)
            .ToListAsync();
        
        return favorites;
    }

    public async Task<bool> AddSong(long userId, long songId)
    {
        var song = await _context.Songs.FindAsync(songId);
        var user = await _context.Users.FindAsync(userId);
        
        if (song == null || user == null)
        {
            return false;
        }

        var favorite = new FavoriteSong
        {
            UserId = userId,
            SongId = songId,
            Song = song
        };
        
         await _context.FavoriteSongs.AddAsync(favorite);
         await _context.SaveChangesAsync();
         
         return true;
    }

    public async Task<bool> AddAlbum(long userId, long albumId)
    {
        var album = await _context.Albums.FindAsync(albumId);
        var user = await _context.Users.FindAsync(userId);

        if (album == null || user == null)
        {
            return false;
        }

        var favorite = new FavoriteAlbum
        {
            UserId = userId,
            AlbumId = albumId,
            Album = album
        };
        
        await _context.FavoriteAlbums.AddAsync(favorite);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteSong(long userId, long songId)
    {
        var favorite = await _context.FavoriteSongs
            .Where(a => a.UserId == userId && a.SongId == songId)
            .Include(f => f.Song)
            .FirstOrDefaultAsync();
        
        if (favorite == null)
        {
            return false;
        }
        
        _context.FavoriteSongs.Remove(favorite);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAlbum(long userId, long albumId)
    {
        var favorite = await _context.FavoriteAlbums
            .Where(a => a.UserId == userId && a.AlbumId == albumId)
            .Include(f => f.Album)
            .FirstOrDefaultAsync();

        if (favorite == null)
        {
            return false;
        }
        
        _context.FavoriteAlbums.Remove(favorite);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
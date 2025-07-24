using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Infrastructure.Repositories;

public class AlbumRepository : IAlbumRepository
{
    private readonly StreamingDbContext _context;

    public AlbumRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Album>> Get()
    {
        var albums = await _context.Albums
            .Include(a => a.Artist)
            .Include(a => a.Songs)
            .ThenInclude(s => s.FeaturingArtists)
            .ThenInclude(s => s.Artist)
            .ToListAsync();
        
        return albums;
    }

    public async Task<Album?> GetById(long id)
    {
        var album = await _context.Albums
            .Include(a => a.Songs)
            .ThenInclude(s => s.FeaturingArtists)
            .ThenInclude(s => s.Artist)
            .Include(a => a.Artist)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        if (album == null) return null;

        return album;
    }
    
    public async Task<List<Album>> GetByArtist(long artistId)
    {
        var albums = await _context.Albums
            .Include(a => a.Artist)
            .Include(s => s.Songs)
            .ThenInclude(s => s.FeaturingArtists)
            .ThenInclude(s => s.Artist)
            .Where(a => a.ArtistId == artistId)
            .ToListAsync();
        
        return albums;
    }
    
}
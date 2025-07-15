using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Models;
using Streaming_service.Domain.Abstractions;

namespace Streaming_service.Infrastructure.Repositories;

public class SongsRepository : ISongsRepository
{
    private readonly StreamingDbContext _context;

    public SongsRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Song>> Get()
    {
        var songs = await _context.Songs
            .Include(a => a.Artist)
            .Include(a => a.Album)
            .Include(a => a.FeaturingArtists)
            .ThenInclude(a => a.Artist)
            .ToListAsync();
        
        return songs;
    }

    public async Task<List<Song>> GetByArtist(int artistId)
    {
        var songs = await _context.Songs
            .Where(a => a.ArtistId == artistId)
            .Include(a => a.Album)
            .Include(a => a.Artist)
            .Include(a => a.FeaturingArtists)
            .ThenInclude(a => a.Artist)
            .ToListAsync();
        
        return songs;
    }
}
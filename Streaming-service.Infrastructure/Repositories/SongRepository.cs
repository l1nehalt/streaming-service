using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Models;
using Streaming_service.Domain.Abstractions;

namespace Streaming_service.Infrastructure.Repositories;

public class SongRepository : ISongRepository
{
    private readonly StreamingDbContext _context;

    public SongRepository(StreamingDbContext context)
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

    public async Task<Song> Create(Song song)
    {
        await _context.Songs.AddAsync(song);
        await _context.SaveChangesAsync();
        return song;
    }
}
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
            .ToListAsync();
        
        return songs;
    }
}
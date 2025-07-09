using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Infrastructure.Repositories;

public class AlbumsRepository : IAlbumsRepository
{
    private readonly StreamingDbContext _context;

    public AlbumsRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Album>> Get()
    {
        var albums = await _context.Albums
            .Include(a => a.Artist)
            .Include(a => a.Songs)
            .ToListAsync();
        
        return albums;
    }
    
}
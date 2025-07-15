using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Infrastructure.Repositories;

public class ArtistsRepository : IArtistsRepository
{
    private readonly StreamingDbContext _context;

    public ArtistsRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Artist>> Get()
    {
        var artists = await _context.Artists.ToListAsync();
        
        return artists;
    }

    public async Task<Artist?> GetById(int id)
    {
        var artist = await _context.Artists
            .Include(a => a.Albums)
            .Include(a => a.Songs)
            .ThenInclude(s => s.FeaturingArtists)
            .ThenInclude(s => s.Artist)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        return artist;
    }
}
using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Infrastructure.Repositories;

public class ArtistRepository : IArtistRepository
{
    private readonly StreamingDbContext _context;

    public ArtistRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Artist>> Get()
    {
        var artists = await _context.Artists.ToListAsync();
        
        return artists;
    }

    public async Task<Artist?> GetByName(string name)
    {
        return await _context.Artists.FirstOrDefaultAsync
            (a => a.Name.ToLower() == name.ToLower());
    }

    public async Task<Artist> Create(Artist artist)
    {
        await _context.Artists.AddAsync(artist);
        await _context.SaveChangesAsync();
        return artist;
    }

    public async Task<Artist?> GetById(long id)
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
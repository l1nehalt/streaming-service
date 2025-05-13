using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;
using Streaming_service.Infrastructure.Mappings;

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
        var albumEntities = await _context.Albums
            .Include(a => a.ArtistEntity)
            .AsNoTracking()
            .ToListAsync();

        var albums = albumEntities.Select(AlbumMapper.ToDomain).ToList();
        
        return albums;
    }

    public async Task<Album?> Search(string query)
    {
        var albumEntity = await _context.Albums
            .AsNoTracking()
            .Include(q => q.ArtistEntity)
            .Include(q => q.SongEntities)
            .FirstOrDefaultAsync(q => q.Title.ToLower().Contains(query.ToLower()));
        
        if (albumEntity == null) return null;

        var album = AlbumMapper.ToDomain(albumEntity);
        
        return album;
    }
}
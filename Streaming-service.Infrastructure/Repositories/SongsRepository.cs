using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Models;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Infrastructure.Mappers;

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
        var songEntities = await _context.Songs
            .AsNoTracking()
            .Include(a => a.ArtistEntity)
            .Include(a => a.AlbumEntity)
            .ToListAsync();

        var songs = songEntities
            .Select(SongMapper.ToDomain).ToList();
        
        return songs;
    }

    public async Task<Song?> Search(string query)
    {
        var songEntity = await _context.Songs
            .AsNoTracking()
            .Include(a => a.ArtistEntity)
            .Include(a => a.AlbumEntity)
            .FirstOrDefaultAsync(q => q.Title.ToLower().Contains(query.ToLower()));

        if (songEntity == null) return null;

        var song = SongMapper.ToDomain(songEntity);
        
        return song;
    }
}
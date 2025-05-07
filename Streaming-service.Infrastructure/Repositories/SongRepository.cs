using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Models;
using AutoMapper;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Infrastructure.Entities;

namespace Streaming_service.Infrastructure.Repositories;

public class SongRepository : ISongRepository
{
    private readonly StreamingDbContext _context;
    private readonly IMapper _mapper;
    
    public SongRepository(StreamingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Song>> GetSongs()
    {
        var songEntities = await _context.Songs
            .Include(a => a.ArtistEntity)
            .Include(a => a.AlbumEntity)
            .ToListAsync();

        return _mapper.Map<List<Song>>(songEntities);
    }
}
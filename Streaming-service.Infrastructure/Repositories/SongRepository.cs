using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Models;
using AutoMapper;
using Streaming_service.Domain.Abstractions;

namespace Streaming_service.Infrastructure.Repositories;

public class SongRepository : ISongRepository
{
    private readonly StreamingDbContext _context;
    private readonly IMapper _mapper;
    
    public SongRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<List<Song>> GetSongs()
    {
        var songEntities = _context.Songs.ToListAsync();
        
        return _mapper.Map<List<Song>>(songEntities);
    }
}
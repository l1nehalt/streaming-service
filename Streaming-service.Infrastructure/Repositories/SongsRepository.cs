using Microsoft.EntityFrameworkCore;
using Streaming_service.Domain.Models;
using AutoMapper;
using Streaming_service.Domain.Abstractions;

namespace Streaming_service.Infrastructure.Repositories;

public class SongsRepository : ISongsRepository
{
    private readonly StreamingDbContext _context;
    private readonly IMapper _mapper;
    
    public SongsRepository(StreamingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Song>> Get()
    {
        var songEntities = await _context.Songs
            .AsNoTracking()
            .Include(a => a.ArtistEntity)
            .Include(a => a.AlbumEntity)
            .ToListAsync();

        return _mapper.Map<List<Song>>(songEntities);
    }

    public async Task<Song?> Search(string query)
    {
        var songEntity = await _context.Songs
            .AsNoTracking()
            .Include(a => a.ArtistEntity)
            .Include(a => a.AlbumEntity)
            .FirstOrDefaultAsync(q => q.Title.ToLower().Contains(query.ToLower()));

        if (songEntity == null) return null;
        
         var song = new Song
         {
           Id = songEntity.Id,
            AlbumId = songEntity.AlbumId,
            ArtistId = songEntity.ArtistId,
            Title = songEntity.Title,
            FeaturingArtists = songEntity.FeaturingArtists,
            FilePath = songEntity.FilePath,
            ImagePath = songEntity.ImagePath,
            IsSingle = songEntity.IsSingle,
            Artist = new Artist
            {
                Id = songEntity.ArtistEntity.Id,
                Name = songEntity.ArtistEntity.Name,
                ImagePath = songEntity.ArtistEntity.ImagePath
            },
            Album = new Album
            {
                Id = songEntity.AlbumEntity.Id,
                ArtistId = songEntity.AlbumEntity.ArtistId,
                ReleaseDate = songEntity.AlbumEntity.ReleaseDate,
                ImagePath = songEntity.AlbumEntity.ImagePath,
                Title = songEntity.AlbumEntity.Title
            }
        };
        
        return song;
    }
}
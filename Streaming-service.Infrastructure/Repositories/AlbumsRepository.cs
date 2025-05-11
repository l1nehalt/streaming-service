using AutoMapper;
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
        var albumEntities = await _context.Albums
            .Include(a => a.ArtistEntity)
            .AsNoTracking()
            .ToListAsync();
        
        var albums = albumEntities.Select(albums => new Album
        {
            Id = albums.Id,
            Title = albums.Title,
            Artist = new Artist
            {
                Id = albums.ArtistEntity.Id,
                Name = albums.ArtistEntity.Name,
                ImagePath = albums.ArtistEntity.ImagePath,
            },
            ReleaseDate = albums.ReleaseDate,
            ArtistId = albums.ArtistId,
            ImagePath = albums.ImagePath,
        }).ToList();
        
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
        
        var album = new Album
        {
            Id = albumEntity.Id,
            ArtistId = albumEntity.ArtistEntity.Id,
            ReleaseDate = albumEntity.ReleaseDate,
            ImagePath = albumEntity.ImagePath,
            Title = albumEntity.Title,
            Artist = new Artist
            {
                Id = albumEntity.ArtistEntity.Id,
                Name = albumEntity.ArtistEntity.Name,
                ImagePath = albumEntity.ImagePath
            },
            Songs = albumEntity.SongEntities.Select(songEntity => new Song
            {
                Id = songEntity.Id,
                ArtistId = songEntity.ArtistId,
                AlbumId = songEntity.AlbumId,
                Title = songEntity.Title,
                FeaturingArtists = songEntity.FeaturingArtists,
                FilePath = songEntity.FilePath,
                ImagePath = songEntity.ImagePath,
                IsSingle = songEntity.IsSingle
            }).ToList()
        };
        
        return album;
    }
}
using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task<List<FavoriteSongDto>> GetFavoriteSongsAsync(long userId)
    {
        var favorites = await _favoriteRepository.GetSongs(userId);

        if (favorites.Count == 0) return new List<FavoriteSongDto>();
        
        var listFavoritesResponse = favorites.Select(favoriteResponse => new FavoriteSongDto
        {
            Id = favoriteResponse.Id,
            ArtistId = favoriteResponse.Song.ArtistId,
            SongTitle = favoriteResponse.Song.Title,
            ArtistName = favoriteResponse.Song.Artist.Name,
            AlbumTitle = favoriteResponse.Song.Album?.Title,
            FilePath = favoriteResponse.Song.FilePath,
            ImagePath = favoriteResponse.Song.ImagePath ?? string.Empty,
            FeaturingArtists = favoriteResponse.Song.FeaturingArtists.Select(fa => new ArtistDto
            {
                Id = fa.ArtistId,
                Name = fa.Artist.Name,
            }).ToList()
        }).ToList();
        
        return listFavoritesResponse;
    }

    public async Task<List<FavoriteAlbumDto>> GetFavoriteAlbumsAsync(long userId)
    {
        var favorites = await _favoriteRepository.GetAlbums(userId);
        
        if (favorites.Count == 0) return new List<FavoriteAlbumDto>();
        
        var listFavoriteResponse = favorites.Select(favoritesResponse => new FavoriteAlbumDto
        {
            Id = favoritesResponse.Id,
            ArtistId = favoritesResponse.Album.ArtistId,
            Title = favoritesResponse.Album?.Title,
            ImagePath = favoritesResponse.Album.ImagePath,
            ArtistName = favoritesResponse.Album.Artist.Name,
            Songs = favoritesResponse.Album.Songs.Select(s => new SongDto
            {
                Id = s.Id,
                Title = s.Title,
                ArtistId = s.ArtistId,
                FilePath = s.FilePath,
                ImagePath = s.ImagePath ?? string.Empty,
                FeaturingArtists = s.FeaturingArtists.Select(fa => new ArtistDto
                {
                    Id = fa.ArtistId,
                    Name = fa.Artist.Name
                }).ToList(),
            }).ToList(),
        }).ToList();
        
        return listFavoriteResponse;
    }

    public async Task<bool> AddFavoriteSongAsync(long userId, long songId)
    {
        return await _favoriteRepository.AddSong(userId, songId);
    }
    
    public async Task<bool> AddFavoriteAlbumAsync(long userId, long albumId)
    {
        return await _favoriteRepository.AddAlbum(userId, albumId);
    }
   
    public async Task<bool> RemoveFavoriteSongAsync(long userId, long songId)
    {
        return await _favoriteRepository.DeleteSong(userId, songId);
    }
    
    public async Task<bool> RemoveFavoriteAlbumAsync(long userId, long albumId)
    {
        return await _favoriteRepository.DeleteAlbum(userId, albumId);
    }
}
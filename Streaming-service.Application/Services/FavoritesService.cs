using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class FavoritesService : IFavoritesService
{
    private readonly IFavoritesRepository _favoritesRepository;

    public FavoritesService(IFavoritesRepository favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
    }

    public async Task<List<FavoriteResponse>> GetFavoritesAsync(long userId)
    {
        var favorites = await _favoritesRepository.Get(userId);

        if (favorites.Count == 0) return new List<FavoriteResponse>();
        
        var listFavoritesResponse = favorites.Select(favoriteResponse => new FavoriteResponse
        {
            Id = favoriteResponse.Id,
            SongTitle = favoriteResponse.Song.Title,
            AlbumTitle = favoriteResponse.Song.Album.Title
        }).ToList();
        
        return listFavoritesResponse;
    }

    public async Task<bool> AddFavoriteAsync(long userId, long songId)
    {
        return await _favoritesRepository.Add(userId, songId);
    }

    public async Task<bool> RemoveFavoriteAsync(long userId, long songId)
    {
        return await _favoritesRepository.Delete(userId, songId);
    }
}
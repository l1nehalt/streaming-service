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

    public async Task<List<Favorite>> GetFavoritesAsync(long userId)
    {
        return await _favoritesRepository.Get(userId);
    }

    public async Task<Favorite?> AddFavoriteAsync(long userId, long songId)
    {
        return await _favoritesRepository.Add(userId, songId);
    }
}
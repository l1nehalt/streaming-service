using Streaming_service.Domain.Abstractions;
using Streaming_service.Domain.Models;

namespace Streaming_service.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<Favorite>> GetFavoritesAsync(long userId)
    {
        return await _userRepository.GetFavorites(userId);
    }

    public async Task<Favorite?> AddFavoriteAsync(long userId, long songId)
    {
        return await _userRepository.AddFavorite(userId, songId);
    }
}
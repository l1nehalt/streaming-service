using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;

namespace streaming_service.Controllers;

[ApiController]
[Route("[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoritesService _favoritesService;

    public FavoritesController(IFavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
    }

    [HttpGet("favorites/{userId}")]
    public async Task<IActionResult> GetFavorites(long userId)
    {
        var favorites = await _favoritesService.GetFavoritesAsync(userId);
        
        return Ok(favorites);
    }

    [HttpPost("addFavorites/{songId}")]
    public async Task<IActionResult> AddFavorite([FromQuery]long userId, long songId)
    {
        var favorite = await _favoritesService.AddFavoriteAsync(userId, songId);

        if (favorite == null)
        {
            return BadRequest();
        }
        
        return Ok(favorite);
    }
}
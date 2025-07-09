using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;


namespace streaming_service.Controllers;

[ApiController]
[Route("api/users/{userId}/favorites")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoritesService _favoritesService;

    public FavoritesController(IFavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserFavorites(long userId)
    {
        var favorites = await _favoritesService.GetFavoritesAsync(userId);
        
        return Ok(favorites);
    }

    [HttpPost("{songId}")]
    public async Task<IActionResult> CreateFavorite(long userId, long songId)
    {
        var result = await _favoritesService.AddFavoriteAsync(userId, songId);

        if (!result)
            return BadRequest(new ProblemDetails {
                Title = "Failed to add favorite",
                Detail = "The song or user might not exist",
                Status = 400
            });
        
        return CreatedAtAction(nameof(GetUserFavorites), 
            new { userId, songId }, null);
    }

    [HttpDelete("{songId}")]
    public async Task<IActionResult> DeleteFavorite(long userId, long songId)
    {
        var result = await _favoritesService.RemoveFavoriteAsync(userId, songId);

        if (!result)
            return NotFound(new ProblemDetails {
                Title = "Favorite not found",
                Detail = "The specified favorite relationship does not exist",
                Status = 404
            });
        
        return NoContent();
    }
}
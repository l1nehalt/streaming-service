using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using streaming_service.Contracts;


namespace streaming_service.Controllers;

[ApiController]
[Route("api/users/{userId}/favorites")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoriteController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserFavorites(long userId)
    {
        var favoriteSongs = await _favoriteService.GetFavoriteSongsAsync(userId);
        var favoriteAlbums = await _favoriteService.GetFavoriteAlbumsAsync(userId);

        var favorites = new FavoriteDto
        {
            FavoriteSongs = favoriteSongs,
            FavoriteAlbums = favoriteAlbums
        };
        
        return Ok(favorites);
    }

    [HttpPost("song/{songId}")]
    public async Task<IActionResult> CreateFavoriteSong(long userId, long songId)
    {
        var result = await _favoriteService.AddFavoriteSongAsync(userId, songId);

        if (!result)
            return BadRequest(new ProblemDetails {
                Title = "Failed to add favorite",
                Detail = "The song or user might not exist",
                Status = 400
            });
        
        return CreatedAtAction(nameof(GetUserFavorites), 
            new { userId, songId }, null);
    }
    
    [HttpPost("album/{albumId}")]
    public async Task<IActionResult> CreateFavoriteAlbum(long userId, long albumId)
    {
        var result = await _favoriteService.AddFavoriteAlbumAsync(userId, albumId);

        if (!result)
            return BadRequest(new ProblemDetails {
                Title = "Failed to add favorite",
                Detail = "The album or user might not exist",
                Status = 400
            });
        
        return CreatedAtAction(nameof(GetUserFavorites), 
            new { userId, albumId }, null);
    }

    [HttpDelete("song/{songId}")]
    public async Task<IActionResult> DeleteFavoriteSong(long userId, long songId)
    {
        var result = await _favoriteService.RemoveFavoriteSongAsync(userId, songId);

        if (!result)
            return NotFound(new ProblemDetails {
                Title = "Favorite not found",
                Detail = "The specified favorite relationship does not exist",
                Status = 404
            });
        
        return NoContent();
    }
    
    [HttpDelete("album/{albumId}")]
    public async Task<IActionResult> DeleteFavoriteAlbum(long userId, long albumId)
    {
        var result = await _favoriteService.RemoveFavoriteAlbumAsync(userId, albumId);

        if (!result)
            return NotFound(new ProblemDetails {
                Title = "Favorite not found",
                Detail = "The specified favorite relationship does not exist",
                Status = 404
            });
        
        return NoContent();
    }
}
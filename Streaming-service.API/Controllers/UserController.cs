using Microsoft.AspNetCore.Mvc;
using Streaming_service.Domain.Abstractions;

namespace streaming_service.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("favorites/{userId}")]
    public async Task<IActionResult> GetFavorites(long userId)
    {
        var favorites = await _userService.GetFavoritesAsync(userId);
        
        return Ok(favorites);
    }

    [HttpPost("addFavorites/{songId}")]
    public async Task<IActionResult> AddFavorite([FromQuery]long userId, long songId)
    {
        var favorite = await _userService.AddFavoriteAsync(userId, songId);

        if (favorite == null)
        {
            return BadRequest();
        }
        
        return Ok(favorite);
    }
}
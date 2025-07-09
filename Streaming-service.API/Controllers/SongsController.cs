using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;

namespace streaming_service.Controllers;

[ApiController]
[Route("/api")]
public class SongsController : ControllerBase
{
    private readonly ISongsService _songsService;

    public SongsController(ISongsService songsService)
    {
        _songsService = songsService;
    }

    [HttpGet("/songs")]
    public async Task<IActionResult> GetSongs()
    {
        var songs = await _songsService.GetSongsAsync();
        
        return Ok(songs);
    }

    [HttpGet("/albums")]
    public async Task<IActionResult> GetAlbums()
    {
        var albums = await _songsService.GetAlbumsAsync();
        
        return Ok(albums);
    }
}
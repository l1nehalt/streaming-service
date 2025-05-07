using Microsoft.AspNetCore.Mvc;
using Streaming_service.Domain.Abstractions;

namespace streaming_service.Controllers;

[ApiController]
[Route("[controller]")]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet("getSongs")]
    public async Task<IActionResult> GetSongs()
    {
        var songs = await _songService.GetSongsAsync();
        
        return Ok(songs);
    }
}
using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;

namespace streaming_service.Controllers;

[ApiController]
[Route("api/")]
public class SongsController : ControllerBase
{
    private readonly ISongsService _songsService;

    public SongsController(ISongsService songsService)
    {
        _songsService = songsService;
    }

    [HttpGet("songs")]
    public async Task<IActionResult> GetSongs()
    {
        var songs = await _songsService.GetSongsAsync();
        
        return songs.Count > 0
                ? Ok(songs)
                : NotFound(new ProblemDetails
                {
                    Title = "Something went wrong",
                    Detail = "Songs not found",
                    Status = 404
                });
    }

    [HttpGet("albums")]
    public async Task<IActionResult> GetAlbums()
    {
        var albums = await _songsService.GetAlbumsAsync();
        
        return albums.Count > 0
            ? Ok(albums)
            : NotFound(new ProblemDetails
            {
                Title = "Something went wrong",
                Detail = "Albums not found",
                Status = 404
            });
    }

    [HttpGet("albums/{id}")]
    public async Task<IActionResult> GetAlbumById(int id)
    {
        var album = await _songsService.GetAlbumByIdAsync(id);
        
        return string.IsNullOrEmpty(album.Error)
            ? Ok(album)
            : BadRequest(new ProblemDetails
            {
                Title = "Something went wrong",
                Detail = album.Error,
                Status = 400
            });
    }
}
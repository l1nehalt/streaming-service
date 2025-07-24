using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.DTOs;
using Streaming_service.Application.Interfaces;
using streaming_service.Contracts;

namespace streaming_service.Controllers;

[ApiController]
[Route("api/")]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet("songs")]
    public async Task<IActionResult> GetSongs()
    {
        var songs = await _songService.GetSongsAsync();

        return songs.Count < 0
            ? NotFound(new ProblemDetails
            {
                Title = "Failed to get songs",
                Detail = "Songs not found",
                Status = 404
            })
            : Ok(songs);
    }

    [HttpGet("albums")]
    public async Task<IActionResult> GetAlbums()
    {
        var albums = await _songService.GetAlbumsAsync();

        return albums.Count < 0
            ? NotFound(new ProblemDetails
            {
                Title = "Failed to get albums",
                Detail = "Albums not found",
                Status = 404
            })
            : Ok(albums);
    }

    [HttpGet("albums/{id}")]
    public async Task<IActionResult> GetAlbumById(long id)
    {
        var album = await _songService.GetAlbumByIdAsync(id);

        return album == null
            ? NotFound(new ProblemDetails
            {
                Title = "Failed to get album",
                Detail = "Album not found",
                Status = 404
            })
            : Ok(album);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadSong([FromForm] UploadSongDto request)
    {
        await _songService.UploadSongAsync
            (request);

        return Ok();
    }
}
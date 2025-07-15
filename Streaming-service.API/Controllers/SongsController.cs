using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;

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
        var albums = await _songsService.GetAlbumsAsync();

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
    public async Task<IActionResult> GetAlbumById(int id)
    {
        var album = await _songsService.GetAlbumByIdAsync(id);

        return album == null
            ? NotFound(new ProblemDetails
            {
                Title = "Failed to get album",
                Detail = "Album not found",
                Status = 404
            })
            : Ok(album);
    }

    [HttpGet("artists/{artistId}/songs")]
    public async Task<IActionResult> GetSongsByArtistId(int artistId)
    {
        var songs = await _songsService.GetSongsByArtistIdAsync(artistId);
        
        return songs.Count < 0 
            ? NotFound(new ProblemDetails
            {
                Title = "Failed to get songs",
                Detail = "Not found songs for artist",
                Status = 404
            })
            : Ok(songs);
    }
    
    [HttpGet("artists/{artistId}/albums")]
    public async Task<IActionResult> GetAlbumsByArtistId(int artistId)
    {
        var albums = await _songsService.GetAlbumsByArtistIdAsync(artistId);
        
        return albums.Count < 0 
            ? NotFound(new ProblemDetails
            {
                Title = "Failed to get albums",
                Detail = "Not found albums for artist",
                Status = 404
            })
            : Ok(albums);
    }
}
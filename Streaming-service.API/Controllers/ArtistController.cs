using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;


namespace streaming_service.Controllers;

[ApiController]
[Route("api/artists")]
public class ArtistController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var artist = await _artistService.GetByIdAsync(id);

        return artist == null
            ? NotFound(new ProblemDetails
            {
                Title = "Failed to get artist",
                Detail = "Artist not found",
                Status = 404
            })
            : Ok(artist);
    }
}
using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;


namespace streaming_service.Controllers;

[ApiController]
[Route("api/artists")]
public class ArtistsController : ControllerBase
{
    private readonly IArtistsService _artistsService;

    public ArtistsController(IArtistsService artistsService)
    {
        _artistsService = artistsService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var artist = await _artistsService.GetByIdAsync(id);

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
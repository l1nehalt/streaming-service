using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;

namespace streaming_service.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery]string query)
    {
        var result = await _searchService.SearchAsync(query);

        if (result == null)
        {
            return NotFound(new ProblemDetails {
                Title = "Not Found",
                Detail = $"No results found for '{query}'",
                Status = 404
            });
        }
        
        return Ok(result);
    }
}
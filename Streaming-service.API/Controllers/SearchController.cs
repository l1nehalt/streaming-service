using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;
using Streaming_service.Domain.Abstractions;

namespace streaming_service.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var result = await _searchService.SearchAsync(query);
        
        return Ok(result);
    }
}
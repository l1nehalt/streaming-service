using Streaming_service.Application.DTOs;

namespace Streaming_service.Application.Interfaces;

public interface ISearchService
{
    Task<SearchDto?> SearchAsync(string query);
}
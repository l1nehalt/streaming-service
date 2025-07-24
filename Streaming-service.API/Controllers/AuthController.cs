using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streaming_service.Application.Interfaces;
using streaming_service.Contracts;
using Streaming_service.Domain.Models;

namespace streaming_service.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
        };
        
        var result = await _authService.RegisterAsync(user);

        if (!result.IsSuccess)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Registration failed",
                Detail = result.Error,
                Status = 400,
            });
        }
        
        return Ok(result);
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Username, request.Password);

        if (!result.IsSuccess)
        {
            return Unauthorized(new ProblemDetails
            {
                Title = "Authorization failed",
                Detail = result.Error,
                Status = 401,
            });
        }
        
        return Ok(result);
    }

    [Authorize]
    [HttpGet("self")]
    public async Task<IActionResult> GetSelf()
    {
        var usernameClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (usernameClaim == null)
        {
            return Unauthorized("username claim missing from token");
        }
        
        var user = await _authService.GetUserProfile(usernameClaim);
        
        return Ok(user);
    }
}
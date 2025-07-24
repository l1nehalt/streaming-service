namespace Streaming_service.Application.DTOs;

public class AuthDto
{
    public object? User { get; set; }
    
    public string Token { get; set; } = string.Empty;
    
    public string Error { get; set; } = string.Empty;
    
    public bool IsSuccess => string.IsNullOrEmpty(Error);

    public static AuthDto Success(object? user, string token) => 
        new AuthDto { User = user, Token = token };
    
    public static AuthDto Success(object? user) => 
        new AuthDto { User = user };

    public static AuthDto Failure(object? user, string error) =>
        new AuthDto { User = user, Error = error };
    
    public static AuthDto Failure(string error) =>
        new AuthDto { Error = error };
}
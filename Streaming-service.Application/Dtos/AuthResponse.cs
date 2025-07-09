namespace Streaming_service.Application.DTOs;

public class AuthResponse
{
    public object? User { get; set; }
    
    public string Token { get; set; } = string.Empty;
    
    public string Error { get; set; } = string.Empty;
    
    public bool IsSuccess => string.IsNullOrEmpty(Error);

    public static AuthResponse Success(object? user, string token) => 
        new AuthResponse { User = user, Token = token };
    
    public static AuthResponse Success(object? user) => 
        new AuthResponse { User = user };

    public static AuthResponse Failure(object? user, string error) =>
        new AuthResponse { User = user, Error = error };
    
    public static AuthResponse Failure(string error) =>
        new AuthResponse { Error = error };
}
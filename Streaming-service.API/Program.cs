using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using streaming_service;
using Streaming_service.Application.Interfaces;
using Streaming_service.Application.Services;
using Streaming_service.Domain.Abstractions; 
using Streaming_service.Infrastructure; 
using Streaming_service.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StreamingDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(StreamingDbContext)));
    });

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var jwtKey = jwtSettings["Key"];
var jwtIssuer = jwtSettings["Issuer"];
var jwtAudience = jwtSettings["Audience"];

var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Streaming Service API",
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите ваш JWT",
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin() 
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IArtistService, ArtistService>();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Streaming Service API V1");
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); 
        c.DisplayRequestDuration(); 
    });
}

app.UseStaticFiles(); 
app.UseCors(); 

app.UseAuthentication();
app.UseAuthorization();  

app.MapControllers();

app.Run();
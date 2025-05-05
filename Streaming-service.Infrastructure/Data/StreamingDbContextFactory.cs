using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Streaming_service.Infrastructure.Data;

public class StreamingDbContextFactory : IDesignTimeDbContextFactory<StreamingDbContext>
{
    public StreamingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StreamingDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=musicians_db;Username=postgres;Password=postgres");

        return new StreamingDbContext(optionsBuilder.Options);
    }
}
using Microsoft.EntityFrameworkCore;
using Specification.Api.Entities;

namespace Specification.Api.DAL;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) 
    : DbContext(options)
{
    public DbSet<ModelEntity> Models { get; set; }
    
    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);


        optionsBuilder
            .EnableSensitiveDataLogging(true)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}
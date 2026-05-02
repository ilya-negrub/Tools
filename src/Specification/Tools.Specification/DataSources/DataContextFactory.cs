using Microsoft.EntityFrameworkCore;

namespace Tools.Specification.DataSources;

internal class DataContextFactory(DbContext dbContext)
{
    public DbContext DbContext => dbContext;
}
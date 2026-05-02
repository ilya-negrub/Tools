using Tools.Specification.Abstraction.Interfaces;

namespace Tools.Specification.DataSources;

internal class SpecDataSource<TEntitySource>(
    DataContextFactory dbContextFactory)
    : ISpecDataSource<TEntitySource>
    where TEntitySource : class
{
    public IQueryable<TEntitySource> GetDataQuery()
        => dbContextFactory.DbContext.Set<TEntitySource>();
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tools.Specification.Abstraction.Interfaces;

namespace Tools.Specification.DataSets;

internal class SpecDataSet<TEntitySource>(IServiceProvider serviceProvider)
    : SpecDataSet<TEntitySource, TEntitySource>(serviceProvider),
        ISpecDataSet<TEntitySource>
    where TEntitySource : class;

internal class SpecDataSet<TEntitySource, TData>(IServiceProvider serviceProvider)
    : ISpecDataSet<TEntitySource, TData>
    where TEntitySource : class
{
    private readonly List<Func<IQueryable<TData>, IQueryable<TData>>> _funcQueries = [];

    public ISpecDataSet<TEntitySource, TData> AddSpec<TSpec>(TSpec spec)
        where TSpec : ISpec<TData>
    {
        var handler = serviceProvider.GetRequiredService<ISpecHandler<TSpec, TData>>();

        _funcQueries.Add(query => handler.Processing(spec, query));

        return this;
    }

    public Task<TData[]> GetDataAsync(CancellationToken ct = default)
    {
        var dataSource = serviceProvider
            .GetRequiredService<ISpecDataSource<TEntitySource, TData>>();

        var query = dataSource.GetDataQuery();

        foreach (var funcQuery in _funcQueries)
        {
            query = funcQuery(query);
        }

        return query.ToArrayAsync(ct);
    }
}
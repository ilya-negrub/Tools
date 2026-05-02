namespace Tools.Specification.Abstraction.Interfaces;

public interface ISpecDataSet<TEntitySource> : ISpecDataSet<TEntitySource, TEntitySource>
    where TEntitySource : class;

// domain
public interface ISpecDataSet<TEntitySource, TData>
    where TEntitySource : class
{
    ISpecDataSet<TEntitySource, TData> AddSpec<TSpec>(TSpec spec)
        where TSpec : ISpec<TData>;

    Task<TData[]> GetDataAsync(CancellationToken ct = default);
}
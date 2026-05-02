namespace Tools.Specification.Abstraction.Interfaces;

public interface ISpecDataSource<TEntity>
    : ISpecDataSource<TEntity, TEntity>
    where TEntity : class;

public interface ISpecDataSource<TEntitySource, TData>
    where TEntitySource : class
{
    public IQueryable<TData> GetDataQuery();
}
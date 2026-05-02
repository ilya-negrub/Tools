namespace Tools.Specification.Abstraction.Interfaces;

public interface ISpecHandler<TSpec, TData>
    where TSpec : ISpec<TData>
{
    IQueryable<TData> Processing(TSpec spec, IQueryable<TData> query);
}
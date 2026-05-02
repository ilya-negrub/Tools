using Specification.Api.Entities;
using Specification.Api.Mdels;
using Specification.Api.Specifications;
using Tools.Specification.Abstraction.Interfaces;

namespace Specification.Api.Handlers;

internal class SpecModelEntityByDateRangeHandler
    : ISpecHandler<SpecModelEntityByDateRange, ModelEntity>,
        ISpecHandler<SpecModelEntityByDateRange, ModelDto>
{
    public IQueryable<ModelEntity> Processing(SpecModelEntityByDateRange spec, IQueryable<ModelEntity> query)
    {
        var begin = spec.Begin;
        var end = spec.End;

        return query
            .Where(x => !begin.HasValue || x.Date >= begin.Value)
            .Where(x => !end.HasValue || x.Date <= end.Value);
    }

    public IQueryable<ModelDto> Processing(SpecModelEntityByDateRange spec, IQueryable<ModelDto> query)
    {
        var begin = spec.Begin;
        var end = spec.End;

        return query
            .Where(x => !begin.HasValue || x.Date >= begin.Value)
            .Where(x => !end.HasValue || x.Date <= end.Value);
    }
}
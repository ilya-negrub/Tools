using Specification.Api.Entities;
using Specification.Api.Mdels;
using Specification.Api.Specifications;
using Tools.Specification.Abstraction.Interfaces;

namespace Specification.Api.Handlers;

internal class SpecModelEntityByNameHandler
    : ISpecHandler<SpecModelEntityByName, ModelEntity>,
        ISpecHandler<SpecModelEntityByName, ModelDto>
{
    public IQueryable<ModelEntity> Processing(SpecModelEntityByName spec, IQueryable<ModelEntity> query)
    {
        return query.Where(x => x.Name == spec.Name);
    }

    public IQueryable<ModelDto> Processing(SpecModelEntityByName spec, IQueryable<ModelDto> query)
    {
        return query.Where(x => x.Name == spec.Name);
    }
}
using Specification.Api.DAL;
using Specification.Api.Entities;
using Specification.Api.Mdels;
using Tools.Specification.Abstraction.Interfaces;

namespace Specification.Api.DataSources;

internal class SpecDataSourceModelEntity(
    AppDatabaseContext dbContext)
    : ISpecDataSource<ModelEntity, ModelDto>
{
    
    public IQueryable<ModelDto> GetDataQuery()
    {
        return dbContext.Set<ModelEntity>()
            .Select(x => new ModelDto()
            {
                Id = x.Id,
                Name = x.Name,
                Date = x.Date
            });
    }
}
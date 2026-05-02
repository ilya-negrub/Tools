using Microsoft.EntityFrameworkCore;
using Specification.Api.DAL;
using Specification.Api.DataSources;
using Specification.Api.Entities;
using Specification.Api.Handlers;
using Specification.Api.Mdels;
using Specification.Api.Specifications;
using Tools.Specification;
using Tools.Specification.Abstraction.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbName = "SpecificationDatabase";
// Регистрируем БД в памяти для использования кэша.
builder.Services
    .AddDbContext<AppDatabaseContext>(opt => opt.UseInMemoryDatabase(dbName));

builder.Services
    .AddSpecifications<AppDatabaseContext>(options =>
    {
        options.AddEntry<ModelEntity>();
        options.AddEntry<ModelEntity, ModelDto>(entry =>
        {
            entry.AddDataSource<SpecDataSourceModelEntity>();

            entry.AddHandlerSpecByEntity<SpecModelEntityByName, SpecModelEntityByNameHandler>();
            entry.AddHandlerSpecByData<SpecModelEntityByName, SpecModelEntityByNameHandler>();

            entry.AddHandlerSpecByEntity<SpecModelEntityByDateRange, SpecModelEntityByDateRangeHandler>();
            entry.AddHandlerSpecByData<SpecModelEntityByDateRange, SpecModelEntityByDateRangeHandler>();
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet(
        "/spec1",
        async (ISpecDataSet<ModelEntity, ModelDto> specDataSet, CancellationToken ct) =>
        {
            specDataSet.AddSpec(new SpecModelEntityByName("Model 2"));
            specDataSet.AddSpec(new SpecModelEntityByDateRange(
                Begin: DateTime.Now,
                End: DateTime.Now.AddHours(5)));

            return await specDataSet.GetDataAsync(ct);
        })
    .WithOpenApi();

app.MapGet(
        "/spec2",
        async (ISpecDataSet<ModelEntity> specDataSet, CancellationToken ct) =>
        {
            specDataSet.AddSpec(new SpecModelEntityByName("Model 2"));
            specDataSet.AddSpec(new SpecModelEntityByDateRange(
                Begin: DateTime.Now,
                End: DateTime.Now.AddHours(15)));

            return await specDataSet.GetDataAsync(ct);
        })
    .WithOpenApi();

app.MapGet(
        "/seed",
        async (AppDatabaseContext dbContext, CancellationToken ct) =>
        {
            dbContext.Models.AddRange([
                new ModelEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Model 1",
                    Date = DateTimeOffset.Now.AddHours(1)
                },
                new ModelEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Model 2",
                    Date = DateTimeOffset.Now.AddHours(2)
                },
                new ModelEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Model 2",
                    Date = DateTimeOffset.Now.AddHours(3)
                },
                new ModelEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Model 2",
                    Date = DateTimeOffset.Now.AddHours(6)
                }
            ]);

            return await dbContext.SaveChangesAsync();
        })
    .WithOpenApi();

app.Run();
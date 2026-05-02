using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tools.Specification.Abstraction.Interfaces;
using Tools.Specification.DataSets;
using Tools.Specification.DataSources;

namespace Tools.Specification;

public static class DI
{
    public static void AddSpecifications<TDbContext>(
        this IServiceCollection services,
        Action<SpecificationsOptions> options)
        where TDbContext : DbContext
    {
        services.AddScoped(typeof(ISpecDataSet<>), typeof(SpecDataSet<>));
        services.AddScoped(typeof(ISpecDataSet<,>), typeof(SpecDataSet<,>));
        services.AddScoped<DataContextFactory>(sp => new DataContextFactory(sp.GetRequiredService<TDbContext>()));
        services.AddScoped(typeof(ISpecDataSource<>), typeof(SpecDataSource<>));

        options?.Invoke(new SpecificationsOptions(services));
    }

    public class SpecificationsOptions(IServiceCollection services)
    {
        public void AddEntry<TEntitySource, TData>(Action<SpecificationsEntryOptions<TEntitySource, TData>> options)
            where TEntitySource : class
        {
            options?.Invoke(new SpecificationsEntryOptions<TEntitySource, TData>(services));
        }

        public void AddEntry<TEntitySource>()
            where TEntitySource : class
        {
            services.AddScoped<ISpecDataSource<TEntitySource, TEntitySource>, SpecDataSource<TEntitySource>>();
        }
    }

    public class SpecificationsEntryOptions<TEntitySource, TData>(IServiceCollection services)
        where TEntitySource : class
    {
        public void AddDataSource<TDataSourceImpl>()
            where TDataSourceImpl : class, ISpecDataSource<TEntitySource, TData>
        {
            services.AddScoped<ISpecDataSource<TEntitySource, TData>, TDataSourceImpl>();
        }

        public void AddHandlerSpecByEntity<TSpec, TImpl>()
            where TSpec : class, ISpec<TEntitySource>
            where TImpl : class, ISpecHandler<TSpec, TEntitySource>
        {
            services.AddScoped<ISpecHandler<TSpec, TEntitySource>, TImpl>();
        }

        public void AddHandlerSpecByData<TSpec, TImpl>()
            where TSpec : class, ISpec<TData>
            where TImpl : class, ISpecHandler<TSpec, TData>
        {
            services.AddScoped<ISpecHandler<TSpec, TData>, TImpl>();
        }
    }
}
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace PMS.Api.Common.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.TryAddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}

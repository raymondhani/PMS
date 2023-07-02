using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using PMS.Api.Common.Errors;
using PMS.Api.Common.Mapping;

namespace PMS.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, PMSProblemDetailsFactory>();
            return services;
        }
    }
}

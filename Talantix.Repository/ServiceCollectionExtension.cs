using Microsoft.Extensions.DependencyInjection;
using Talantix.ModelsLibrary.Interfaces;

namespace Talantix.Repository
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddTransient<IDbProvider, DbProvider>();

            return service;
        }
    }
}

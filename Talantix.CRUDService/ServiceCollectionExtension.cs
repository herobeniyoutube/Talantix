using Microsoft.Extensions.DependencyInjection;
using Talantix.ModelsLibrary.Interfaces;
using Talantix.Repository;

namespace Talantix.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCRUDService(this IServiceCollection service)
        {
            service.AddSingleton<ICRUDService, CRUDService>();

            return service;
        }
    }
}

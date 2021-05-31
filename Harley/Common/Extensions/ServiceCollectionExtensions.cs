using System.Linq;
using System.Reflection;
using Harley.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Harley.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHarleyServices(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetCustomAttribute<HarleyService>() != null);

            foreach (var type in types)
                services.AddSingleton(type);

            return services;
        }
    }
}
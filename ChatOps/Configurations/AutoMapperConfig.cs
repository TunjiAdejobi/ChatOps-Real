using ChatOps.Model.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChatOps.API.Configurations
{
    public static class AutoMapperConfig
    {
        public static void Configure(IServiceCollection services, params Assembly[] additionalAssemblies)
        {
            services.AddAutoMapper(additionalAssemblies);
            services.AddAutoMapper(typeof(UserProfileRequestMappingConfig));
        }
    }
}

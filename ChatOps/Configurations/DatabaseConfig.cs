using ChatOps.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace ChatOps.API.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ChatOpsDbContext>(opt =>
            {
                opt.UseSqlServer(config["Database:ChatOpsConnectionString"]);
            });            
        }


        public static void Configure(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ChatOpsDbContext>();
                context.Database.Migrate(); // Apply pending migrations
            }
        }
    }
}

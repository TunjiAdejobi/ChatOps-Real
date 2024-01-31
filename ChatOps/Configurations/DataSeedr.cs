using ChatOps.Data.DataContext;
using ChatOps.Model.Models;

namespace ChatOps.API.Configurations
{
    public class DataSeedr : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DataSeedr(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ChatOpsDbContext>();
                SeedData(context);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // No cleanup needed
            return Task.CompletedTask;
        }

        private void SeedData(ChatOpsDbContext context)
        {
            // Add your data seeding logic here
            if (!context.Occupations.Any())
            {
                var occupation = new List<Occupation>
                {
                    new Occupation { Name = "EntrepreneurShip", Industry = "Business" },
                    new Occupation { Name = "Product Management", Industry = "InfoTech" },
                    new Occupation { Name = "Accounting", Industry = "Finance" },
                     new Occupation { Name = "Journalism", Industry = "Media" },                    
                    // Add more as needed
                };

                context.Occupations.AddRange(occupation);
                context.SaveChanges();
            }
        }
    }
}

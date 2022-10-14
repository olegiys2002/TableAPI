using BookingTables.Core.Services;
using Quartz;

namespace BookingTables.API.ServicesConfiguration
{
    public static class QuartzExtensions
    {
        public static void ConfigureQuartz(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.AddJobAndTrigger<QuartzService>(configuration);
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        }
    }
}

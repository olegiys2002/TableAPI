using Quartz;

namespace BookingTables.API.ServicesConfiguration
{
    public static class JobsAndTriggersForQuartz
    {
        public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration config) where T : IJob
        {
            var jobName = typeof(T).Name;

            var crons = config["Quartz:QuartzService"];

            var jobKey = new JobKey(jobName);
            quartz.AddJob<T>(opts=>opts.WithIdentity(jobKey));  
            quartz.AddTrigger(opts => opts.ForJob(jobKey).WithIdentity(jobName + "-trigger").WithCronSchedule(crons));
            
        }
    }

}

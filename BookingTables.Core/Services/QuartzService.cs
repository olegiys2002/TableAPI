using Core.IServices;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BookingTables.Core.Services
{
    public class QuartzService : IJob
    {
        private readonly ILogger<QuartzService> _logger;
  
        public QuartzService(ILogger<QuartzService> logger)
        {
          _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Our app works");
            return Task.CompletedTask;
        }
    }
}

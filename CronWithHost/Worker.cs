using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace CronWithHost
{
    public class Worker : IWorker
    {
        private readonly ILogger<Worker> logger;
        private readonly IHostEnvironment hostEnvironment;

        public Worker(ILogger<Worker> logger, IHostEnvironment hostEnvironment)
        {
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
        }

        public Task RunAsync()
        {
            if (hostEnvironment.IsEnvironment("Development"))
            {
                logger.LogInformation($"Current application name is {hostEnvironment.ApplicationName}\n" +
                $"      Current content root path is {hostEnvironment.ContentRootPath}\n" +
                $"      Current environment is {hostEnvironment.EnvironmentName}");
            }
            return Task.CompletedTask;
        }
    }
}
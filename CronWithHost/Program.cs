using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Threading.Tasks;

namespace CronWithHost
{
    public static class Program
    {
        private static IHost host;
        public static IServiceProvider ServiceProvider { get; private set; }

        public static async Task Main(string[] args)
        {
            host = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configBuilder => configBuilder.AddJsonFile($"appsettings.secrets.json", optional: true))
                .ConfigureServices(async (context, services) => await ConfigureAppServicesAsync(context, services))
                .Build();

            ServiceProvider = host.Services;

            await StartHostAsync();

            await RunAppAsync();

            await StopHostAsync();
        }

        private static Task ConfigureAppServicesAsync(HostBuilderContext context, IServiceCollection services)
        {
            services.AddTransient<IWorker, Worker>();
            return Task.CompletedTask;
        }

        private static async Task StartHostAsync()
        {
            await host.StartAsync();
        }

        private static async Task RunAppAsync()
        {
            IWorker folderService = ServiceProvider.GetRequiredService<IWorker>();
            await folderService.RunAsync();
        }

        private static async Task StopHostAsync()
        {
            using (host)
            {
                await host.StopAsync();
            }
        }
    }
}
using CrossplatformService.Interfaces;
using Microsoft.Extensions.Hosting;

namespace CrossplatformService.Linux
{
    public class LinuxHostedService : BackgroundService
    {
        public IService mService;

        public LinuxHostedService(IService service)
        {
            mService = service;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    mService.Update();

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

            }, stoppingToken);

            return Task.CompletedTask;
        }
    }
}

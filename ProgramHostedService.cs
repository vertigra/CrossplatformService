using CrossplatformService.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace CrossplatformService
{
    public class ProgramHostedService : IHostedService, IDisposable
    {
        public ProgramHostedService(IHostApplicationLifetime applicationLifetime, IService service)
        {  
        }

        public void Dispose()
        {
            Debug.WriteLine("App Dispose");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("App start");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("App stop");
            return Task.CompletedTask;
        }
    }
}

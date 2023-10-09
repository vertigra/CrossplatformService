using CrossplatformService.Interfaces;
using CrossplatformService.Linux;
using CrossplatformService.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace CrossplatformService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    builder.Services.AddSingleton<IService>(new LinuxService());
                    builder.Services.AddHostedService(serviceProvider => new LinuxHostedService(serviceProvider.GetService<IService>()));
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    builder.Services.AddSingleton<IService>(new WindowsService());
                    builder.Services.AddHostedService(serviceProvider => new WindowsHostedService(serviceProvider.GetService<IService>()));
                }

                builder.Services.AddHostedService(serviceProvider => new ProgramHostedService(
                    serviceProvider.GetService<IHostApplicationLifetime>(), serviceProvider.GetService<IService>()));
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

            using IHost host = builder.Build();
            host.RunAsync().Wait();
            host.WaitForShutdownAsync();
        }
    }
}
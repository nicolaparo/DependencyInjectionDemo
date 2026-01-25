using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace DependencyInjectionDemo.Scenarios;

public class HostScenarios(ITestOutputHelper output)
{
    public class WorkerBackgroundService(ITestOutputHelper output, string workerName) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            output.WriteLine($"Worker {workerName} is working.");
            return Task.CompletedTask;
        }
    }
    public class NitwithBackgroundService(ITestOutputHelper output, string workerName) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            output.WriteLine($"Nitwith {workerName} is doing nothing.");
            return Task.CompletedTask;
        }
    }


    [Fact]
    public async Task HostBuilder_MultipleHostedService_Issue()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddHostedService(sp => new WorkerBackgroundService(output, "Jimmy"));
        builder.Services.AddHostedService(sp => new WorkerBackgroundService(output, "Jhonatan"));
        builder.Services.AddHostedService(sp => new NitwithBackgroundService(output, "Noah"));

        var app = builder.Build();

        await app.StartAsync();
        await Task.Delay(1000);
        await app.StopAsync();
    }

    [Fact]
    public async Task HostBuilder_MultipleHostedService_Fixed()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddSingleton<IHostedService>(sp => new WorkerBackgroundService(output, "Jimmy"));
        builder.Services.AddSingleton<IHostedService>(sp => new WorkerBackgroundService(output, "Jhonatan"));
        builder.Services.AddSingleton<IHostedService>(sp => new NitwithBackgroundService(output, "Noah"));

        var app = builder.Build();

        await app.StartAsync();
        await Task.Delay(1000);
        await app.StopAsync();
    }
}
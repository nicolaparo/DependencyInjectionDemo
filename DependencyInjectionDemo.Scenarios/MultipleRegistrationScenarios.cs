using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DependencyInjectionDemo.Scenarios;

public class MultipleRegistrationScenarios(ITestOutputHelper output)
{
    public interface IGreetingService
    {
        string GetGreetingMessageFor(string name);
    }

    public class AliceGreetingService : IGreetingService
    {
        public string GetGreetingMessageFor(string name)
            => $"Hi, {name}! Welcome to Alice's greeting service.";
    }
    public class BobGreetingService : IGreetingService
    {
        public string GetGreetingMessageFor(string name)
            => $"Hello, {name}! This is Bob's greeting service.";
    }
    public class ChrisGreetingService : IGreetingService
    {
        public string GetGreetingMessageFor(string name)
            => $"Hey, {name}! Chris here, glad to greet you.";
    }


    [Fact]
    public void Register_With_AddSingleton()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddTransient<IGreetingService, AliceGreetingService>();
        services.AddSingleton<IGreetingService, BobGreetingService>();
        services.AddSingleton<IGreetingService, ChrisGreetingService>();

        var serviceProvider = services.BuildServiceProvider();

        var greetingService = serviceProvider.GetRequiredService<IGreetingService>();

        var message = greetingService.GetGreetingMessageFor("John");

        output.WriteLine(message);
    }

    [Fact]
    public void Register_With_TryAddSingleton()
    {
        ServiceCollection services = new ServiceCollection();

        services.TryAddSingleton<IGreetingService, AliceGreetingService>();
        services.TryAddSingleton<IGreetingService, BobGreetingService>();
        services.TryAddSingleton<IGreetingService, ChrisGreetingService>();

        var serviceProvider = services.BuildServiceProvider();

        var greetingService = serviceProvider.GetRequiredService<IGreetingService>();
        var message = greetingService.GetGreetingMessageFor("John");

        output.WriteLine(message);
    }

    [Fact]
    public void RegisterMultiple_With_AddSingleton_And_ResolveAll()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IGreetingService, AliceGreetingService>();
        services.AddSingleton<IGreetingService, BobGreetingService>();
        services.AddSingleton<IGreetingService, ChrisGreetingService>();

        var serviceProvider = services.BuildServiceProvider();
        var greetingServices = serviceProvider.GetServices<IGreetingService>();

        foreach (var service in greetingServices)
        {
            var message = service.GetGreetingMessageFor("John");
            output.WriteLine(message);
        }
    }

    [Fact]
    public void RegisterMultiple_With_TryAddSingleton_And_ResolveAll()
    {
        ServiceCollection services = new ServiceCollection();

        services.TryAddSingleton<IGreetingService, AliceGreetingService>();
        services.TryAddSingleton<IGreetingService, BobGreetingService>();
        services.TryAddSingleton<IGreetingService, ChrisGreetingService>();

        var serviceProvider = services.BuildServiceProvider();
        var greetingServices = serviceProvider.GetServices<IGreetingService>();

        foreach (var service in greetingServices)
        {
            var message = service.GetGreetingMessageFor("John");
            output.WriteLine(message);
        }
    }

    [Fact]
    public void Register_With_AddKeyedSingleton()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddKeyedTransient<IGreetingService, AliceGreetingService>("alice");
        services.AddKeyedSingleton<IGreetingService, BobGreetingService>("bob");
        services.AddKeyedSingleton<IGreetingService, ChrisGreetingService>("chris");

        var serviceProvider = services.BuildServiceProvider();

        var greetingService = serviceProvider.GetRequiredKeyedService<IGreetingService>("bob");

        var message = greetingService.GetGreetingMessageFor("John");

        output.WriteLine(message);
    }

    public class GreetingConsumer(IGreetingService greetingService)
    {
        public string Greet(string name)
        {
            return greetingService.GetGreetingMessageFor(name);
        }
    }

    [Fact]
    public void Register_SingletonAndScoped()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IGreetingService, AliceGreetingService>();
        services.AddScoped<IGreetingService, BobGreetingService>();
        services.AddSingleton<GreetingConsumer>();

        var serviceProvider = services.BuildServiceProvider();

        var greetingService = serviceProvider.GetRequiredService<GreetingConsumer>();

        var message = greetingService.Greet("John");

        output.WriteLine(message);
    }

    [Fact]
    public void Register_SingletonAndScoped_But_WithValidation()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IGreetingService, AliceGreetingService>();
        services.AddScoped<IGreetingService, BobGreetingService>();
        services.AddSingleton<GreetingConsumer>();

        // var serviceProvider = services.BuildServiceProvider(validateScopes: true);
        var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions()
        {
            //ValidateOnBuild = true,
            ValidateScopes = true
        });

        Assert.Throws<InvalidOperationException>(() =>
        {
            var greetingService = serviceProvider.GetRequiredService<GreetingConsumer>();

            var message = greetingService.Greet("John");

            output.WriteLine(message);
        });
    }

    [Fact]
    public async Task Register_SingletonAndScoped_Correctly_WithValidation()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IGreetingService, AliceGreetingService>();
        services.AddScoped<IGreetingService, BobGreetingService>();
        services.AddScoped<GreetingConsumer>();
        var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions()
        {
            ValidateOnBuild = true,
            ValidateScopes = true
        });

        await using (var scope = serviceProvider.CreateAsyncScope())
        {
            var greetingService = scope.ServiceProvider.GetRequiredService<GreetingConsumer>();
            var message = greetingService.Greet("John");
            output.WriteLine(message);
        }
    }


}

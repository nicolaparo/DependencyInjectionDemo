# Dependency Injection Demo

A demonstration solution showcasing dependency injection patterns in .NET, featuring both .NET Framework WinForms and modern .NET implementations.

## 📋 Overview

This solution contains practical examples of Microsoft.Extensions.DependencyInjection usage across different .NET platforms and scenarios, helping developers understand how to implement dependency injection in both legacy and modern .NET applications.

## 🏗️ Projects

### DependencyInjectionDemo.NetFrameworkAspNet

A .NET Framework ASP.NET Web API application demonstrating dependency injection in a legacy ASP.NET Framework context.

**Key Features:**
- Integration of `Microsoft.Extensions.DependencyInjection` in .NET Framework 4.x Web API
- Custom dependency resolver bridging Microsoft.Extensions.DI with Web API
- Service lifetime management (Scoped services for Web API)
- Controller injection through custom DI resolver
- RESTful API endpoints with injected services

**Components:**
- `Global.asax.cs` - Application entry point with DI container setup
- `WebApiDependencyResolver.cs` - Custom IDependencyResolver implementation for Web API
- `ServiceCollectionExtensions.cs` - Extension methods for controller registration
- `ValuesController.cs` - Web API controller with constructor injection
- `ValuesService.cs` - Example service registered as scoped
- `IValuesService.cs` - Service abstraction interface

### DependencyInjectionDemo.NetFrameworkWinForm

A .NET Framework Windows Forms application demonstrating dependency injection in a legacy WinForms context.

**Key Features:**
- Integration of `Microsoft.Extensions.DependencyInjection` in .NET Framework 4.x
- Service lifetime management (Singleton, Transient)
- Form injection through service provider
- Cross-form service sharing via DI container

**Components:**
- `Program.cs` - Application entry point with DI container setup
- `Form1.cs` - Main form with service provider injection
- `Form2.cs` - Secondary form demonstrating form navigation via DI
- `GreetingService.cs` - Example service registered as singleton

### DependencyInjectionDemo.Scenarios

A .NET 10.0 test project containing unit tests that demonstrate various dependency injection scenarios and patterns.

**Test Scenarios:**
- **HostScenarios** - IHostedService registration patterns and common pitfalls
  - Multiple hosted service registration issues
  - Proper IHostedService registration techniques
  
- **MultipleRegistrationScenarios** - Service registration patterns
  - Multiple service implementations
  - Service lifetime behaviors (Transient, Singleton, Scoped)
  - Service resolution strategies

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022 or later
- .NET Framework 4.7.2+ (for WinForms project)
- .NET 10.0 SDK (for Scenarios project)

### Running the ASP.NET Web API Application

1. Open `DependencyInjectionDemo.slnx` in Visual Studio
2. Set `DependencyInjectionDemo.NetFrameworkAspNet` as the startup project
3. Press F5 to run

The application demonstrates:
- Dependency injection in ASP.NET Web API controllers
- Custom dependency resolver integration
- Scoped service lifetime in web requests
- RESTful API with GET, POST, PUT, DELETE endpoints

### Running the WinForms Application

1. Open `DependencyInjectionDemo.slnx` in Visual Studio
2. Set `DependencyInjectionDemo.NetFrameworkWinForm` as the startup project
3. Press F5 to run

The application demonstrates:
- Service injection into forms
- Navigation between forms using DI
- Shared service state across form instances

### Running the Tests

1. Open Test Explorer in Visual Studio
2. Build the solution
3. Run all tests in `DependencyInjectionDemo.Scenarios`

## 📦 NuGet Packages

### Core Dependencies
- `Microsoft.Extensions.DependencyInjection` (10.0.2)
- `Microsoft.Extensions.DependencyInjection.Abstractions` (10.0.2)
- `Microsoft.Extensions.Hosting` (10.0.2) - for host scenarios

### Web Dependencies (ASP.NET Project)
- `Microsoft.AspNet.WebApi` (5.2.9)
- `Microsoft.AspNet.WebApi.Core` (5.2.9)
- `Microsoft.AspNet.WebApi.WebHost` (5.2.9)
- `Microsoft.AspNet.Mvc` (5.2.9)
- `Newtonsoft.Json` (13.0.3)

### Supporting Libraries
- `Microsoft.Bcl.AsyncInterfaces` (10.0.2)
- `System.Runtime.CompilerServices.Unsafe` (6.1.2)
- `System.Threading.Tasks.Extensions` (4.6.3)

## 🎯 Key Concepts Demonstrated

### Service Lifetimes
- **Singleton** - Single instance shared across application lifetime
- **Transient** - New instance created for each request
- **Scoped** - One instance per scope (demonstrated in scenarios)

### DI Container Setup

**ASP.NET Web API:**
```csharp
var services = new ServiceCollection();
services.AddControllers();
services.AddScoped<IValuesService, ValuesService>();
var serviceProvider = services.BuildServiceProvider();
GlobalConfiguration.Configuration.DependencyResolver = new WebApiDependencyResolver(serviceProvider);
```

**WinForms:**
```csharp
var services = new ServiceCollection();
services.AddSingleton<GreetingService>();
services.AddTransient<Form1>();
var serviceProvider = services.BuildServiceProvider();
```

### Service Resolution
```csharp
var service = serviceProvider.GetRequiredService<IGreetingService>();
```

## 🧪 Testing Patterns

The Scenarios project uses **xUnit** for unit testing and demonstrates:
- Testing DI container configurations
- Verifying service resolution behavior
- Testing multiple service registrations
- IHostedService integration testing

## 📝 Notes

- The ASP.NET Web API project shows how to integrate Microsoft.Extensions.DependencyInjection with legacy ASP.NET Framework Web API
- The WinForms project shows how to modernize legacy .NET Framework applications with DI
- The Scenarios project provides test cases that can serve as documentation for common DI patterns
- All projects use the same DI abstractions, showing cross-platform compatibility
- Custom dependency resolvers bridge the gap between Microsoft.Extensions.DI and older .NET Framework DI systems

## 🤝 Contributing

This is a demonstration project. Feel free to explore the code and adapt patterns for your own projects.

## 📄 License

This project is provided as-is for educational purposes.

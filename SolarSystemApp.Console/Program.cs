using SolarSystemApp.Console.Constants;
using SolarSystemApp.Console.Domain.Services.Interfaces;
using SolarSystemApp.Console.Domain.Services;
using SolarSystemApp.Console.Utilities;
using Microsoft.Extensions.DependencyInjection;
using log4net.Config;
using log4net;
using System.Reflection;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        //The ConfigureServices function configures the services.
        ConfigureServices(serviceCollection);

        //The RunServiceOperations function executes the code that can create the outputs.
        RunServiceOperations(serviceCollection);
    }

    private static void RunServiceOperations(IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();

        //The service provider gets the services.
        var screenOutputService = serviceProvider.GetService<IOutputService>();

        try
        {
            screenOutputService.OutputAllPlanetsAndTheirAverageMoonGravityToConsole();
            screenOutputService.OutputAllMoonsAndTheirMassToConsole();
            screenOutputService.OutputAllPlanetsAndTheirMoonsToConsole();
        }
        catch (Exception exception)
        {
            //The users and developers can see the thrown exceptions.
            Logger.Instance.Error($"{LoggerMessage.ScreenOutputOperationFailed}{exception.Message}");
            System.Console.WriteLine($"{ExceptionMessage.ScreenOutputOperationFailed}{exception.Message}");
            System.Diagnostics.Debug.WriteLine($"{ExceptionMessage.ScreenOutputOperationFailed}{exception.Message}");
        }

        serviceProvider.Dispose();
    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
        //The function configures all the services.
        XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()),
            new FileInfo(ConfigurationFileName.Logger));
        serviceCollection.AddHttpClient<HttpClientService>();
        serviceCollection.AddSingleton<IPlanetService, PlanetService>();
        serviceCollection.AddSingleton<IOutputService, ScreenOutputService>();
        serviceCollection.AddSingleton<IMoonService, MoonService>();
    }
}

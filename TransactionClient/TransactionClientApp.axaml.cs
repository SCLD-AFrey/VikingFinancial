using System;
using System.IO;
using System.Net;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using Grpc.Core;

using Microsoft.Extensions.Logging;

using Serilog.Events;
using Serilog.Formatting.Json;
using ValkyrieFinancial.Protos.Transactions;
using VikingFinancial.Common;


using TransactionClient.ViewModels;
using TransactionClient.Views;
using TransactionClient.Models;
using TransactionClient.Models.DataStructures;
using TransactionClient.Models.Interceptors;
using TransactionClient.Models.Main;
using TransactionClient.Models.ServerInteraction;
using TransactionClient.Models.Services;
using TransactionClient.ViewModels.Main;
using TransactionClient.Views.Main;

namespace TransactionClient;

public partial class TransactionClientApp : Application
{
    private readonly IHost m_appHost;
    public static ServerCredentials? ServerCredentials { get; set; }
    
    public TransactionClientApp()
    {
        ServerCredentials = new ServerCredentials()
        {
            ServerAddress = Constants.HostAddress,
            ServerPort    = Constants.Ports.TransactionServicePortSecure
        };

        m_appHost = Host.CreateDefaultBuilder()
            .ConfigureLogging(p_options =>
            {
                p_options.AddDebug();
                p_options.AddSerilog();
            })
            .ConfigureServices(ConfigureServices).Build();
    }

    private void ConfigureServices(IServiceCollection p_services)
    {
        p_services.AddTransient<ErrorHandlerInterceptor>();
        p_services.AddSingleton<Folders>();
        p_services.AddSingleton<Files>();
        p_services.AddSingleton<ServerInfo>();
        p_services.AddSingleton<ClientProvisioner>();
        p_services.AddSingleton<Connectivity>();

        p_services.AddSingleton<MainWindow>();
        p_services.AddSingleton<MainWindowViewModel>();
        p_services.AddSingleton<MainWindowModel>();
        
        p_services.AddSingleton<DetailsView>();
        p_services.AddSingleton<DetailsViewModel>();
        p_services.AddSingleton<DetailsModel>();
        
        p_services.AddSingleton<TransactionsView>();
        p_services.AddSingleton<TransactionsViewModel>();
        p_services.AddSingleton<TransactionsModel>();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        var logLevel = LogEventLevel.Debug;

        var filesService = m_appHost.Services.GetRequiredService<Files>();
    
        Log.Logger = new LoggerConfiguration()
                     .MinimumLevel.Is(logLevel)
                     .WriteTo.Sink(new CollectionSink())
                     .WriteTo.RollingFile(new JsonFormatter(), filesService.LogFilePath, retainedFileCountLimit: 31)
                     .CreateLogger();
    
        Log.Logger.Information("Starting application");
    
        await m_appHost.StartAsync();
    
        if ( ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop )
        {
            desktop.ShutdownRequested += DesktopOnShutdownRequested;
            desktop.MainWindow = m_appHost.Services.GetService<MainWindow>();
        }
    
        base.OnFrameworkInitializationCompleted();
    }

    private async void DesktopOnShutdownRequested(object? p_sender, ShutdownRequestedEventArgs p_e)
    {
        await m_appHost.StopAsync();
    }
}
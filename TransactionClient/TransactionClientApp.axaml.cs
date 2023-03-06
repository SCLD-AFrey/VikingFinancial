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
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;

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
    
        AppDomain.CurrentDomain.UnhandledException += (_, p_eventArgs) =>
                                                      {
                                                          Log.Debug("Unhandled Exception: {ErrorMessage}",
                                                                    ((Exception)p_eventArgs.ExceptionObject).Message);
                                                      };
    
        AppDomain.CurrentDomain.FirstChanceException += async (_, p_eventArgs) =>
                                                        {
                                                            Log.Debug("First chance exception: {ErrorMessage}",
                                                                      p_eventArgs.Exception.Message);
    
                                                          
                                                            await Dispatcher.UIThread.InvokeAsync(async () =>
                                                            {
                                                                var assemblyName = Assembly.GetEntryAssembly()!.GetName().Name;
                                                                var assets       = AvaloniaLocator.Current.GetService<IAssetLoader>();
                                                                var windowIcon   = new Bitmap(assets!.Open(new Uri($"avares://{assemblyName}/Assets/newShield.ico")));
                                                                
                                                                var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                                                                    .GetMessageBoxCustomWindow(new MessageBoxCustomParams
                                                                        {
                                                                            ContentTitle = "Commander Disconnected",
                                                                            WindowIcon = new WindowIcon(windowIcon),
                                                                            ContentMessage = "Commander has become unreachable. Client will close.",
                                                                            ButtonDefinitions = new[]
                                                                                {
                                                                                    new ButtonDefinition
                                                                                    {
                                                                                        Name      = "Exit",
                                                                                        IsDefault = true
                                                                                    }
                                                                                },
                                                                            Icon                  = Icon.Error,
                                                                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                                                            CanResize             = false,
                                                                            Topmost               = true,
                                                                            ShowInCenter          = true,
                                                                            SystemDecorations     = SystemDecorations.Full
                                                                        });
                                                                await messageBoxStandardWindow.Show();
                                                            });
                                                            Environment.Exit(0);
                                                        };
    
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
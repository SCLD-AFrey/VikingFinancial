using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MessageBox.Avalonia.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System.Threading.Tasks;
using VikingFinancial.Gui.Models.BackingModels.MainApplication;
using VikingFinancial.Gui.Models.Services;
using VikingFinancial.Gui.Models.Services.Database;
using VikingFinancial.Gui.ViewModels;
using VikingFinancial.Gui.ViewModels.MainApplication;
using VikingFinancial.Gui.Views;
using VikingFinancial.Gui.Views.MainApplication;

namespace VikingFinancial.Gui
{
    public partial class VikingFinGuiApp : Application
    {
        private readonly IHost m_appHost;

        public VikingFinGuiApp()
        {
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

            p_services.AddSingleton<Folders>();
            p_services.AddSingleton<Files>();
            p_services.AddSingleton<Crypto>();
            
            p_services.AddSingleton<DatabaseUtilities>();
            p_services.AddSingleton<TransactionDatabaseInterface>();
            p_services.AddSingleton<TransactionDatabaseInitialization>();
            
            p_services.AddSingleton<MainWindowView>();
            p_services.AddSingleton<MainWindowModel>();
            p_services.AddSingleton<MainWindowViewModel>();
            p_services.AddSingleton<MainWorkspaceModel>();
            
            p_services.AddSingleton<LandingPageModel>();
            
            p_services.AddSingleton<MainWorkspaceModel>();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public override async void OnFrameworkInitializationCompleted()
        {
#if DEBUG
            var logLevel = LogEventLevel.Debug;
#else
        var logLevel = LogEventLevel.Information;
#endif

            var filesService = m_appHost.Services.GetRequiredService<Files>();

            // Set Serilog sinks. - Comment by Matt Heimlich on 03/07/2022 @ 13:59:06
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                //.WriteTo.Sink(new CollectionSink())
                .WriteTo.File(new JsonFormatter(), filesService.LogFilePath, retainedFileCountLimit: 31)
                .CreateLogger();

            await m_appHost.StartAsync();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.ShutdownRequested += DesktopOnShutdownRequested;

                await InitializeApplication(desktop);


                //TODO COME BACK TO THIS
                desktop.MainWindow = m_appHost.Services.GetService<MainWindowView>();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private async Task InitializeApplication(IClassicDesktopStyleApplicationLifetime p_desktop)
        {
            await DoFirstTimeSetup();
        }

        private async Task DoFirstTimeSetup()
        { 
            var dbInitializationService = m_appHost.Services.GetRequiredService<TransactionDatabaseInitialization>();
            await dbInitializationService.DoFirstTimeSetup();
        }

        private async void DesktopOnShutdownRequested(object? p_sender, ShutdownRequestedEventArgs p_e)
        {
            await m_appHost.StopAsync();
        }
    }
}

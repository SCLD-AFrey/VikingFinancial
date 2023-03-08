using System.Data;
using System.Net;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Services;
using TransactionService.Services.Database;
using TransactionService.Services.Global;
using VikingFinancial.Common;

namespace TransactionService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker>                  m_logger;
        private readonly BackgroundWorkerHeartbeatMonitor m_monitor;

        public Worker(ILogger<Worker> p_logger, BackgroundWorkerHeartbeatMonitor p_monitor)
        {
            m_logger  = p_logger;
            m_monitor = p_monitor;
        }

        protected override async Task ExecuteAsync(CancellationToken p_stoppingToken)
        {
#pragma warning disable CS4014
            m_monitor.Start(p_stoppingToken);
#pragma warning restore CS4014

            m_logger.LogInformation("Starting Transaction Service");

            await StartServer(p_stoppingToken);
        }

        public override Task StopAsync(CancellationToken p_stoppingToken)
        {
            m_logger.LogInformation("Stopping Transaction Service");

            return base.StopAsync(p_stoppingToken);
        }

        private async Task StartServer(CancellationToken p_cancellationToken)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddGrpc();
            
            builder.Services.AddSingleton<CommonDirectories>();
            builder.Services.AddSingleton<CommonFiles>();
            builder.Services.AddSingleton<PasswordCrypto>();
            
            
            builder.Services.AddSingleton<DatabaseUtilities>();
            builder.Services.AddSingleton<TransactionDatabaseInterface>();
            builder.Services.AddSingleton<TransactionDatabaseInit>();
            var commonFilesService = builder.Services.BuildServiceProvider().GetRequiredService<CommonFiles>();

            builder.Logging.AddFile(commonFilesService.WorkerLogsPath, 
                minimumLevel: LogLevel.Information, 
                levelOverrides: new Dictionary<string, LogLevel>
                {
                    { "Grpc.AspNetCore.Server", LogLevel.Warning },
                    { "Microsoft.AspNetCore", LogLevel.Warning }
                },
                isJson: true, 
                retainedFileCountLimit: 31);

            builder.WebHost.ConfigureKestrel(p_options =>
            {
                p_options.ListenAnyIP(Constants.Ports.TransactionServicePortSecure,
                    p_listenOptions =>
                    {
                        p_listenOptions.Protocols = Microsoft
                            .AspNetCore.Server
                            .Kestrel.Core
                            .HttpProtocols.Http2;
                        p_listenOptions.UseHttps(StoreName.Root,
                            "ConfigOS Server Root CA",
                            true,
                            StoreLocation
                                .LocalMachine);
                    }
                );
            });
            
            var appHost = builder.Build();

            var dbInitializationService = appHost.Services.GetRequiredService<TransactionDatabaseInit>();
            await dbInitializationService.DoFirstTimeSetup();
            
            
            appHost.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();
            
            appHost.MapGrpcService<Services.TransactionService>();
            appHost.MapGrpcService<Services.ConnectivityService>();
            appHost.MapGrpcService<Services.UserService>();
            
            await appHost.RunAsync(p_cancellationToken);
        }
    }
}
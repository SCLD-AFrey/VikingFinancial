using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TransactionClientGui.Services.Grpc;

public class BackgroundWorkerHeartbeatMonitor
{
    public BackgroundWorkerHeartbeatMonitor(ILogger<BackgroundWorkerHeartbeatMonitor> p_logger)
    {
        m_logger = p_logger;
    }

    private readonly ILogger<BackgroundWorkerHeartbeatMonitor> m_logger;
    
    public async Task Start(CancellationToken p_stoppingToken)
    {
        while ( !p_stoppingToken.IsCancellationRequested )
        {
            m_logger.LogInformation("Transaction Service service is running");
            await Task.Delay(120000, p_stoppingToken);
        }
    }
}
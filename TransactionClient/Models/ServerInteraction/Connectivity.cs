using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using TransactionClient.Models.Services;

namespace TransactionClient.Models.ServerInteraction;

public class Connectivity
{
    private readonly ILogger<Connectivity> m_logger;
    private readonly ServerInfo m_serverInfo;
    private readonly ClientProvisioner m_clientProvisionerService;

    public Connectivity(ILogger<Connectivity> p_logger,
        ServerInfo p_serverInfo,
        ClientProvisioner p_clientProvisionerService)
    {
        m_logger = p_logger;
        m_serverInfo = p_serverInfo;
        m_clientProvisionerService = p_clientProvisionerService;
        m_logger.LogDebug("Initialized Connection class");
    }
    
    public async Task TestServerConnectivityAsync()
    {
        var connectivityClient = m_clientProvisionerService.ProvisionClient<ValkyrieFinancial.Protos.Connectivity.Connectivity.ConnectivityClient>();

        m_logger.LogDebug("Testing Server Connectivity");
        await connectivityClient.CheckServerConnectionAsync(new Empty());
    }

    public void TestServerConnectivity()
    {
        var connectivityClient = m_clientProvisionerService.ProvisionClient<ValkyrieFinancial.Protos.Connectivity.Connectivity.ConnectivityClient>();

        m_logger.LogDebug("Testing Server Connectivity");
        connectivityClient.CheckServerConnection(new Empty());
    }
}
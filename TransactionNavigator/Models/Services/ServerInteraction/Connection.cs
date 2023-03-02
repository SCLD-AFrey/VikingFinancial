﻿using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using ValkyrieFinancial.Protos.Connectivity;
using Google.Protobuf.WellKnownTypes;

namespace TransactionNavigator.Models.Services.ServerInteraction;

public class Connection
{
    private readonly ILogger<Connection> m_logger;
    private readonly ServerInfo          m_serverInfo;
    private readonly ClientProvisioner   m_clientProvisionerService;
    
    public Connection(ILogger<Connection> p_logger, 
        ServerInfo p_serverInfo, 
        ClientProvisioner p_clientProvisionerService)
    {
        m_logger                        = p_logger;
        m_serverInfo                    = p_serverInfo;
        m_clientProvisionerService = p_clientProvisionerService;
        m_logger.LogDebug("Initialized Connection class");
    }
    
    public async Task TestServerConnectivityAsync()
    {
        var connectivityClient = m_clientProvisionerService.ProvisionClient<Connectivity.ConnectivityClient>();

        m_logger.LogDebug("Testing Server Connectivity");
        await connectivityClient.CheckServerConnectionAsync(new Empty());
    }
    
    public void TestServerConnectivity()
    {
        var connectivityClient = m_clientProvisionerService.ProvisionClient<Connectivity.ConnectivityClient>();

        m_logger.LogDebug("Testing Server Connectivity");
        connectivityClient.CheckServerConnection(new Empty());
    }
}
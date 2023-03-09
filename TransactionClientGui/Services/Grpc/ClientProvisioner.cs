using System.Net.Http;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using ValkyrieFinancial.Protos;

namespace TransactionClientGui.Services.Grpc;

public class ClientProvisioner
{
    private readonly ILogger<ClientProvisioner> m_logger;
    private readonly ServerInfo m_serverInfoService;
    private readonly GrpcChannel m_channel;

    public ClientProvisioner(ServerInfo p_serverInfoService, ILogger<ClientProvisioner> p_logger)
    {
        m_serverInfoService = p_serverInfoService;
        m_logger = p_logger;
        
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        
        m_channel = GrpcChannel.ForAddress("https://127.0.0.1:50002",
            new GrpcChannelOptions { HttpHandler = httpHandler });
        
    }

    public Connectivity.ConnectivityClient ProvisionConnectivityClient()
    {
        return new Connectivity.ConnectivityClient(m_channel);
    }

    public TransactionsService.TransactionsServiceClient ProvisionTransactionClient()
    {
        return new TransactionsService.TransactionsServiceClient(m_channel);
    }
}
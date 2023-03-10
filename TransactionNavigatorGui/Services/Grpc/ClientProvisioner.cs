using System.Net.Http;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using ValkyrieFinancial.Protos;

namespace TransactionNavigatorGui.Services.Grpc;

public class ClientProvisioner
{
    private readonly GrpcChannel m_channel;
    public ClientProvisioner()
    {
        
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
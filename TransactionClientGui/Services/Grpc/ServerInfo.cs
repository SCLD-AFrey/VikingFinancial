using System.Net.Http;
using Grpc.Core;
using Grpc.Net.Client;

namespace TransactionClientGui.Services.Grpc;

public class ServerInfo
{
    public ServerInfo()
    {
    }
    private           GrpcChannel?            Channel { get; set; }
    public            CallInvoker?            Invoker { get; private set; }
    
    public void SetChannel()
    {

        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        
        Channel = GrpcChannel.ForAddress("https://127.0.0.1:50002", new GrpcChannelOptions { HttpHandler = httpHandler });
        Invoker = Channel.CreateCallInvoker();
    }
    
}
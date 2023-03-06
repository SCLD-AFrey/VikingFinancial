using System;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using TransactionClient.Models.ServerInteraction;

namespace TransactionClient.Models.Services;

public class ClientProvisioner
{
    private ILogger<ClientProvisioner> m_logger;
    private readonly ServerInfo m_serverInfoService;

    public ClientProvisioner(ServerInfo p_serverInfoService,
        ILogger<ClientProvisioner> p_logger)
    {
        m_serverInfoService = p_serverInfoService;
        m_logger = p_logger;
    }

    public T ProvisionClient<T>() where T : ClientBase<T>
    {
        return (T)Activator.CreateInstance(typeof(T), m_serverInfoService.Invoker)!;
    }
}
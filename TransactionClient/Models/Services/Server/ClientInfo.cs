using Microsoft.Extensions.Logging;

namespace TransactionClient.Models.Services.Server;
public class ClientInfo
{
    public ClientInfo(ILogger<ClientInfo> p_logger)
    {
        p_logger.LogDebug("Initialized ClientInfo class");
    }
    
    public string? SessionId { get; set; }
}
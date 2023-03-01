using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ValkyrieFinancial.Protos.Authorization;

namespace TransactionClient.Models.Services.Server;

public class Authorization
{
    private readonly ILogger<Authorization> m_logger;
    private readonly ClientProvisioner      m_clientProvisionerService;
    private readonly ClientInfo             m_clientInfo;

    public Authorization(ILogger<Authorization> p_logger,
        ClientInfo             p_clientInfo,
        ClientProvisioner      p_clientProvisionerService)
    {
        m_logger                   = p_logger;
        m_clientInfo               = p_clientInfo;
        m_clientProvisionerService = p_clientProvisionerService;
        m_logger.LogDebug("Initialized Authorization class");
    }

    public async Task<G_AuthorizationResponse> RequestAuthorizationAsync(string p_userName)
    {
        m_logger.LogDebug("Requested Authorization");
        var authorizationClient =
            m_clientProvisionerService.ProvisionClient<ValkyrieFinancial.Protos.Authorization.Authorization.AuthorizationClient>();

        return await authorizationClient.GetAuthorizationTokenAsync(new G_AuthorizationRequest
        {
            SessionId = m_clientInfo.SessionId,
            Username  = p_userName
        });
    }

    public G_AuthorizationResponse RequestAuthorization(string p_userName)
    {
        m_logger.LogDebug("Requested Authorization");
        var authorizationClient =
            m_clientProvisionerService.ProvisionClient<ValkyrieFinancial.Protos.Authorization.Authorization.AuthorizationClient>();

        return authorizationClient.GetAuthorizationToken(new G_AuthorizationRequest
        {
            SessionId = m_clientInfo.SessionId,
            Username  = p_userName
        });
    }
}
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ValkyrieFinancial.Protos.Authentication;

namespace TransactionClient.Models.Services.Server;

public class Authentication
{
    private readonly ILogger<Authentication> m_logger;
    private readonly ClientProvisioner       m_clientProvisionerService;
    
    public Authentication(ILogger<Authentication> p_logger,
                          ClientProvisioner p_clientProvisionerService)
    {
        m_logger                        = p_logger;
        m_clientProvisionerService = p_clientProvisionerService;
    }

    public async Task<G_AuthenticationResponse> ValidateCredentialsAsync(string p_userName, string p_password)
    {
        m_logger.LogInformation("Attempting server login");

        var authenticationClient =
            m_clientProvisionerService.ProvisionClient<ValkyrieFinancial.Protos.Authentication.Authentication.AuthenticationClient>();
        
        return await authenticationClient.ValidateCredentialsAsync(new G_AuthenticationRequest
                                                   {
                                                       Username = p_userName,
                                                       Password = p_password
                                                   });
    }
    
    public G_AuthenticationResponse ValidateCredentials(string p_userName, string p_password)
    {
        m_logger.LogInformation("Attempting server login");

        var authenticationClient =
            m_clientProvisionerService.ProvisionClient<ValkyrieFinancial.Protos.Authentication.Authentication.AuthenticationClient>();
        
        return authenticationClient.ValidateCredentials(new G_AuthenticationRequest
                                                                   {
                                                                       Username = p_userName,
                                                                       Password = p_password
                                                                   });
    }
    
    public async Task<G_PasswordExpirationResponse> CheckPasswordExpirationAsync(string p_userName)
    {
        m_logger.LogInformation("Checking password expiration");

        var authenticationClient =
            m_clientProvisionerService.ProvisionClient<ValkyrieFinancial.Protos.Authentication.Authentication.AuthenticationClient>();
        
        return await authenticationClient.CheckPasswordExpirationAsync(new G_PasswordExpirationRequest
                                                   {
                                                       Username = p_userName
                                                   });
    }
    
    public G_PasswordExpirationResponse CheckPasswordExpiration(string p_userName)
    {
        m_logger.LogInformation("Checking password expiration");

        var authenticationClient =
            m_clientProvisionerService.ProvisionClient<ValkyrieFinancial.Protos.Authentication.Authentication.AuthenticationClient>();
        
        return authenticationClient.CheckPasswordExpiration(new G_PasswordExpirationRequest
                                                                       {
                                                                           Username = p_userName
                                                                       });
    }
}
using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransactionClient.Models.DataStructures;
using ValkyrieFinancial.Protos.Authentication;
using ValkyrieFinancial.Protos.Authorization;
using ValkyrieFinancial.Protos.LoginDataLoginData;

namespace TransactionClient.Models.Services.Server;

public class Login
{
    private readonly ILogger<Login>    m_logger;
    private readonly Connection        m_connectionService;
    private readonly Authentication    m_authenticationService;
    private readonly Authorization     m_authorizationService;
    private readonly ServerInfo        m_serverInfo;
    private readonly ClientInfo        m_clientInfo;
    private readonly UserInfo          m_userInfo;
    private readonly ClientProvisioner m_clientProvisionerService;

    public Login(ILogger<Login>    p_logger,
                 Connection        p_connectionService,
                 Authentication    p_authenticationService,
                 Authorization     p_authorizationService,
                 ServerInfo        p_serverInfo,
                 ClientInfo        p_clientInfo,
                 UserInfo          p_userInfo,
                 ClientProvisioner p_clientProvisionerService)
    {
        m_logger                   = p_logger;
        m_connectionService        = p_connectionService;
        m_authenticationService    = p_authenticationService;
        m_authorizationService     = p_authorizationService;
        m_serverInfo               = p_serverInfo;
        m_clientInfo               = p_clientInfo;
        m_userInfo                 = p_userInfo;
        m_clientProvisionerService = p_clientProvisionerService;
        m_logger.LogDebug("Initialized Login class");
    }

    public async Task AttemptLoginAsync(ServerLoginCredentials p_credentials)
    {
        m_logger.LogDebug("Attempting Login");
        m_serverInfo.SetUnauthenticatedChannel(p_credentials.ServerAddress, p_credentials.ServerPort);

        await m_connectionService.TestServerConnectivityAsync();

        var credentialValidationResponse =
            await m_authenticationService.ValidateCredentialsAsync(p_credentials.Username, p_credentials.Password);

        switch ( credentialValidationResponse.ResponseCase )
        {
            case G_AuthenticationResponse.ResponseOneofCase.Failure:
                throw credentialValidationResponse.Failure.FailureResponseCase switch
                      {
                          G_AuthenticationFailureResponse.FailureResponseOneofCase.BadCredentials =>
                              new InvalidCredentialException("Login credentials could not be validated"),
                          G_AuthenticationFailureResponse.FailureResponseOneofCase.TooManyAttempts =>
                              new
                                  AuthenticationException($"Too many failed logon attempts. You may try again at {credentialValidationResponse.Failure.TooManyAttempts.UnlockTime.ToDateTimeOffset().LocalDateTime:hh:mm:ss}"),
                          G_AuthenticationFailureResponse.FailureResponseOneofCase.AlreadyLoggedIn =>
                              new AuthenticationException($"User '{p_credentials.Username}' is already logged into the server"),
                          G_AuthenticationFailureResponse.FailureResponseOneofCase.None => new
                              ArgumentOutOfRangeException(nameof(credentialValidationResponse.Failure.FailureResponseCase),
                                                          "Failure response type not specified"),
                          _ => new ArgumentOutOfRangeException(nameof(credentialValidationResponse.Failure.FailureResponseCase),
                                                               "Unknown failure response type encountered")
                      };
            case G_AuthenticationResponse.ResponseOneofCase.Success:
                m_clientInfo.SessionId = credentialValidationResponse.Success.SessionId;
                break;
            case G_AuthenticationResponse.ResponseOneofCase.None:
                throw new ArgumentOutOfRangeException(nameof(credentialValidationResponse.ResponseCase),
                                                      "Login response type not specified");
            default:
                throw new ArgumentOutOfRangeException(nameof(credentialValidationResponse.ResponseCase),
                                                      "Unknown login response type encountered");
        }

        m_serverInfo.SetUnauthenticatedChannelWithCredentials(p_credentials.Username, m_clientInfo.SessionId);
        
        var authorizationResponse = await m_authorizationService.RequestAuthorizationAsync(p_credentials.Username);

        switch ( authorizationResponse.ResponseCase )
        {
            case G_AuthorizationResponse.ResponseOneofCase.Success:
                m_userInfo.SetCurrentUser(User.GenerateUserFromServerResponse(p_credentials.Username, authorizationResponse.Success.Token));

                m_serverInfo.SetAuthenticatedChannel(authorizationResponse.Success.Token, p_credentials.Username, m_clientInfo.SessionId);
                break;
            case G_AuthorizationResponse.ResponseOneofCase.Failure:
                throw new AuthenticationException("Could not acquire authorization token from server");
            case G_AuthorizationResponse.ResponseOneofCase.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        m_logger.LogDebug("Calling to Check Password Expiration");
        var passwordExpiredResponse = await m_authenticationService.CheckPasswordExpirationAsync(m_userInfo.CurrentUser!.Username);

        switch ( passwordExpiredResponse.PasswordExpirationResponseCase )
        {
            case G_PasswordExpirationResponse.PasswordExpirationResponseOneofCase.NotExpired:
                break;
            case G_PasswordExpirationResponse.PasswordExpirationResponseOneofCase.None:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task GetPostLoginDataAsync()
    {
        m_logger.LogDebug("Retrieving Post Login Data");
        var postLoginClient = m_clientProvisionerService.ProvisionClient<LoginData.LoginDataClient>();
        var loginData = await postLoginClient.GetPostLoginDataAsync(new G_LoginDataRequest
                                                                    {
                                                                        Username = m_userInfo.CurrentUser!.Username
                                                                    });

        m_userInfo.LastSuccessfulLoginDate           = loginData.LastSuccessfulLoginDate.ToDateTimeOffset().LocalDateTime;
        m_userInfo.FailedLoginAttemptsSinceLastLogin = loginData.NumberOfFailedLoginAttempts;
    }

    public void GetPostLoginData()
    {
        m_logger.LogDebug("Retrieving Post Login Data");
        var postLoginClient = m_clientProvisionerService.ProvisionClient<LoginData.LoginDataClient>();
        var loginData = postLoginClient.GetPostLoginData(new G_LoginDataRequest
                                                         {
                                                             Username = m_userInfo.CurrentUser!.Username
                                                         });

        m_userInfo.LastSuccessfulLoginDate           = loginData.LastSuccessfulLoginDate.ToDateTimeOffset().LocalDateTime;
        m_userInfo.FailedLoginAttemptsSinceLastLogin = loginData.NumberOfFailedLoginAttempts;
    }
}
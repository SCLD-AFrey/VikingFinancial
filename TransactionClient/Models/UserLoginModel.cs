using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransactionClient.Models.DataStructures;
using TransactionClient.Models.Services;
using TransactionClient.Models.Services.Server;

namespace TransactionClient.Models;

public class UserLoginModel
{
    private readonly ILogger<UserLoginModel>          m_logger;
    private readonly ServerInfo                       m_serverInfoService;
    private readonly Files                            m_files;
    private readonly Login m_loginService;

    public UserLoginModel(ILogger<UserLoginModel> p_logger, Files p_files, ServerInfo p_serverInfoService, Login p_loginService)
    {
        m_logger = p_logger;
        m_files = p_files;
        m_serverInfoService = p_serverInfoService;
        m_loginService = p_loginService;
        m_logger.LogDebug("Instantiating UserLoginModel");
    }
    
    public int GetLoginDelayTime(int p_attemptedLoginCount)
    {
        return p_attemptedLoginCount switch
        {
            >= 3 and < 6 => 3000,
            >= 6 and < 8 => 6000,
            >= 8         => 10000,
            _            => 0
        };
    }

    public ClientConfiguration? GetClientConfig()
    {
        return m_files.GetClientConfig();
    }

    public async Task SaveClientConfig(string p_serverAddress, string p_port)
    {
        await m_files.SaveClientConfig(p_serverAddress, p_port);
        m_logger.LogDebug("Saving configuration values {Server}, {Port}, and {ServerPairingKeyPath}", p_serverAddress, p_port);
    }

    public void CloseCommandCenter()
    {
        Environment.Exit(0);
    }

    public async Task AttemptLogin(ServerLoginCredentials p_serverLoginObject)
    {
        await m_loginService.AttemptLoginAsync(p_serverLoginObject);
    }

    public async Task GetPostLoginData()
    {
        await m_loginService.GetPostLoginDataAsync();
    }
}
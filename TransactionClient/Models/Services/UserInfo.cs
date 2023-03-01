using System;
using Microsoft.Extensions.Logging;
using TransactionClient.Models.DataStructures;
using VikingFinancial.Data.Transaction;

namespace TransactionClient.Models.Services;

public class UserInfo
{
    private readonly ILogger<UserInfo> m_logger;
    
    public UserInfo(ILogger<UserInfo> p_logger)
    {
        m_logger = p_logger;
        
        m_logger.LogDebug("Initializing UserInfo service");
    }

    public void SetCurrentUser(User p_user)
    {
        m_logger.LogDebug("Setting current user to user '{NewCurrentUser}'", p_user.Username);
        
        CurrentUser = p_user;
    }
    
    public User?          CurrentUser                       { get; private set; }
    public DateTimeOffset LastSuccessfulLoginDate           { get; set; }
    public int            FailedLoginAttemptsSinceLastLogin { get; set; }
    public bool           MustChangePassword                { get; set; }
}
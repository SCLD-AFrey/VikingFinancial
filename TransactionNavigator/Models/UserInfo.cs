using System;
using Microsoft.Extensions.Logging;
using VikingFinancial.Data.Transaction;

namespace TransactionNavigator.Models;

public class UserInfo
{
    private readonly ILogger<UserInfo> m_logger;
    
    public UserInfo(ILogger<UserInfo> p_logger)
    {
        m_logger = p_logger;
        
        m_logger.LogDebug("Initializing UserInfo service");
    }

    public void SetCurrentUser(UserProfile p_user)
    {
        m_logger.LogDebug("Setting current user to user '{NewCurrentUser}'", p_user.Username);
        
        CurrentUser = p_user;
    }
    
    public UserProfile?          CurrentUser                       { get; private set; }
    public DateTimeOffset LastSuccessfulLoginDate           { get; set; }
}
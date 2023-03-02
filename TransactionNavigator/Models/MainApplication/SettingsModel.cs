using Microsoft.Extensions.Logging;
using TransactionNavigator.Models.Services;

namespace TransactionNavigator.Models.MainApplication;

public class SettingsModel
{
    private readonly ILogger<SettingsModel> m_logger;
    public SettingsModel(ILogger<SettingsModel> p_logger)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating SettingsModel");
    }
    
}
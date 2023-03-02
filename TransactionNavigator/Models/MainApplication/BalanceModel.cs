using Microsoft.Extensions.Logging;
using TransactionNavigator.Models.Services;

namespace TransactionNavigator.Models.MainApplication;

public class BalanceModel
{
    private readonly ILogger<BalanceModel> m_logger;
    public BalanceModel(ILogger<BalanceModel> p_logger)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating BalanceModel");
    }
    
}
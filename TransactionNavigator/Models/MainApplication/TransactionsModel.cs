using Microsoft.Extensions.Logging;
using TransactionNavigator.Models.Services;

namespace TransactionNavigator.Models.MainApplication;

public class TransactionsModel
{
    private readonly ILogger<TransactionsModel> m_logger;
    public TransactionsModel(ILogger<TransactionsModel> p_logger)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating TransactionsModel");
    }
    
}
using Microsoft.Extensions.Logging;

namespace TransactionClient.Models;

public class TransactionModel
{
    private readonly ILogger<TransactionModel> m_logger;
    
    public TransactionModel(ILogger<TransactionModel> p_logger)
    {
        m_logger = p_logger;
    }
}
using Microsoft.Extensions.Logging;

namespace TransactionClient.Models.Main;

public class TransactionsModel
{
    private readonly ILogger<TransactionsModel>      m_logger;

    public TransactionsModel(ILogger<TransactionsModel> p_logger)
    {
        m_logger = p_logger;
    }
}
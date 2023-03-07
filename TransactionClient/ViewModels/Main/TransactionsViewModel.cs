using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace TransactionClient.ViewModels.Main;

public class TransactionsViewModel : ReactiveObject
{
    private readonly ILogger<TransactionsViewModel> m_logger;

    public TransactionsViewModel(ILogger<TransactionsViewModel> p_logger)
    {
        m_logger = p_logger;
    }
}
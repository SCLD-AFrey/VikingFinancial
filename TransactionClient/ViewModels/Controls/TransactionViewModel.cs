using Microsoft.Extensions.Logging;
using TransactionClient.Models;

namespace TransactionClient.ViewModels.Controls;

public class TransactionViewModel : ViewModelBase
{
    private readonly ILogger<TransactionViewModel> m_logger;
    public TransactionViewModel(ILogger<TransactionViewModel> p_logger,
        TransactionModel p_model)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Instantiating TransactionViewModel");
        Model = p_model;
    }
    private TransactionModel Model { get; }
}
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace TransactionNavigatorGui.ViewModels.MainApplication;

public class BalanceViewModel : ViewModelBase
{
    private readonly ILogger<BalanceViewModel> m_logger;

    public BalanceViewModel(ILogger<BalanceViewModel> p_logger)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Instantiating BalanceViewModel");
    }
    
    [Reactive] public string PageHeaderText { get; set; } = "Balance";
}
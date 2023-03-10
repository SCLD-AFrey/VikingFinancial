using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace TransactionNavigatorGui.ViewModels.MainApplication;

public class WelcomeViewModel : ViewModelBase
{
    private readonly ILogger<WelcomeViewModel> m_logger;

    public WelcomeViewModel(ILogger<WelcomeViewModel> p_logger)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Instantiating WelcomeViewModel");
    }
    
    [Reactive] public string PageHeaderText { get; set; } = "Welcome to Transaction Navigator";
}
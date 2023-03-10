using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace TransactionNavigatorGui.ViewModels.MainApplication;

public class SettingsViewModel : ViewModelBase
{
    private readonly ILogger<SettingsViewModel> m_logger;

    public SettingsViewModel(ILogger<SettingsViewModel> p_logger)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Instantiating SettingsViewModel");
    }
    
    [Reactive] public string PageHeaderText { get; set; } = "Settings";
}
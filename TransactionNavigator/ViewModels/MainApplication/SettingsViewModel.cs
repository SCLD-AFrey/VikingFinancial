using System;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;

namespace TransactionNavigator.ViewModels.MainApplication;

public class SettingsViewModel : ViewModelBase
{
    private readonly ILogger<SettingsViewModel> m_logger;

    public SettingsViewModel(ILogger<SettingsViewModel> p_logger,
        SettingsViewModel p_model)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating SettingsViewModel");

        Model = p_model;
        
        PageHeaderText = "Balance View";

    }
    public event EventHandler UserIsEditingChanged = delegate { };
    
    private SettingsViewModel Model { get; }
    [Reactive] public string PageHeaderText { get; private set; }
    [Reactive] public bool UserIsEditing { get; private set; }

    public void OnSelectedPageChanged(object? p_sender, EventArgs p_e)
    {
        if (p_sender is not MainWorkspaceViewModel { SelectedPageIndex: 0 }) return;
    }
    
}
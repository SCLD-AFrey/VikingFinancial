using System;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace TransactionNavigator.ViewModels.MainApplication;

public class BalanceViewModel : ViewModelBase
{
    private readonly ILogger<BalanceViewModel> m_logger;

    public BalanceViewModel(ILogger<BalanceViewModel> p_logger,
        BalanceViewModel p_model)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating BalanceViewModel");

        Model = p_model;
        
        PageHeaderText = "Balance View";

    }
    public event EventHandler UserIsEditingChanged = delegate { };
    
    private BalanceViewModel Model { get; }
    [Reactive] public string PageHeaderText { get; private set; }
    [Reactive] public bool UserIsEditing { get; private set; }

    public void OnSelectedPageChanged(object? p_sender, EventArgs p_e)
    {
        if (p_sender is not MainWorkspaceViewModel { SelectedPageIndex: 0 }) return;
    }
    
}
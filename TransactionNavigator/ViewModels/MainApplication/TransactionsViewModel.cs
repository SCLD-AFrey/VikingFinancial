using System;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;

namespace TransactionNavigator.ViewModels.MainApplication;

public class TransactionsViewModel : ViewModelBase
{
    private readonly ILogger<TransactionsViewModel> m_logger;

    public TransactionsViewModel(ILogger<TransactionsViewModel> p_logger,
        TransactionsViewModel p_model)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating TransactionsViewModel");

        Model = p_model;
        
        PageHeaderText = "Balance View";

    }
    public event EventHandler UserIsEditingChanged = delegate { };
    
    private TransactionsViewModel Model { get; }
    [Reactive] public string PageHeaderText { get; private set; }
    [Reactive] public bool UserIsEditing { get; private set; }

    public void OnSelectedPageChanged(object? p_sender, EventArgs p_e)
    {
        if (p_sender is not MainWorkspaceViewModel { SelectedPageIndex: 0 }) return;
    }
    
}
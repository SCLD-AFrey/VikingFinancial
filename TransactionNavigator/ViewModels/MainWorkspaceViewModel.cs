using System;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TransactionNavigator.Models;
using TransactionNavigator.ViewModels.MainApplication;
using TransactionNavigator.Views.MainApplication;

namespace TransactionNavigator.ViewModels;

public class MainWorkspaceViewModel : ViewModelBase
{
    private readonly ILogger<MainWorkspaceViewModel> m_logger;
    
    public MainWorkspaceViewModel(ILogger<MainWorkspaceViewModel> p_logger,
        MainWorkspaceModel p_model,
        SettingsView p_settingsView,
        SettingsViewModel p_settingsViewModel,
        BalanceView p_balanceView,
        BalanceViewModel p_balanceViewModel,
        TransactionsView p_transactionsView,
        TransactionsViewModel p_transactionsViewModel)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating MainWorkspaceViewModel");

        Model = p_model;
        
        SettingsView = p_settingsView;
        BalanceView = p_balanceView;
        TransactionsView = p_transactionsView;

        p_settingsViewModel.UserIsEditingChanged += (_, _) => UserIsEditing = !UserIsEditing;
        p_balanceViewModel.UserIsEditingChanged += (_, _) => UserIsEditing = !UserIsEditing;
        p_transactionsViewModel.UserIsEditingChanged += (_, _) => UserIsEditing = !UserIsEditing;
        
        SelectedPageChanged += p_settingsViewModel.OnSelectedPageChanged;
        SelectedPageChanged += p_balanceViewModel.OnSelectedPageChanged;
        SelectedPageChanged += p_transactionsViewModel.OnSelectedPageChanged;

        this.WhenAnyValue(p_vm => p_vm.SelectedPageIndex).Subscribe(OnSelectedPageIndexChanged);
    }
    
    private void OnSelectedPageIndexChanged(int p_index)
    {
        SelectedPageChanged(this, EventArgs.Empty);
    }
    
    public void ClickExpandNavigationPanel()
    {
        PaneIsOpen = !PaneIsOpen;
    }
    
    public void ClickNavigationButton(object p_parameter)
    {
        if ( p_parameter is not string stringParameter ) return;
        
        m_logger.LogDebug("Clicked main workspace navigation button '{NavigationTarget:l}'", stringParameter);

        Model.SetNavigation(stringParameter);
    }

    public event EventHandler SelectedPageChanged = delegate { };

    private MainWorkspaceModel Model { get; }
    
    public SettingsView SettingsView { get; set; }
    public TransactionsView TransactionsView { get; set; }
    public BalanceView BalanceView { get; set; }
    
    [Reactive] internal bool UserIsEditing { get; set; }
    [Reactive] public bool                                       PaneIsOpen    { get; set; }
    [Reactive] public int                                        SelectedPageIndex  { get; set; }
}
using System;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using VikingFinancial.Gui.Models.BackingModels.MainApplication;

namespace VikingFinancial.Gui.ViewModels.MainApplication.MainWindow;

public class MainWorkspaceViewModel : ViewModelBase
{
    private readonly ILogger<MainWorkspaceViewModel> m_logger;

    public MainWorkspaceViewModel(ILogger<MainWorkspaceViewModel> p_logger,
        MainWorkspaceModel p_model)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating MainWorkspaceViewModel");

        Model = p_model;
        
        this.WhenAnyValue(p_vm => p_vm.SelectedPageIndex).Subscribe(OnSelectedPageIndexChanged);
    }
    
    private void OnSelectedPageIndexChanged(int p_index)
    {
        SelectedPageChanged(this, EventArgs.Empty);
    }

    public event EventHandler SelectedPageChanged = delegate { };

    private MainWorkspaceModel Model { get; }
    [Reactive] internal bool UserIsEditing { get; set; }
    [Reactive] public bool                                       PaneIsOpen    { get; set; }
    [Reactive] public int                                        SelectedPageIndex  { get; set; }
    
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
}
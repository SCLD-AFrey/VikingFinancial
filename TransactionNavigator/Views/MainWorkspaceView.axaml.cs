using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using TransactionNavigator.ViewModels;

namespace TransactionNavigator.Views;

public partial class MainWorkspaceView : UserControl
{
    private readonly ILogger<MainWorkspaceView> m_logger;

#pragma warning disable CS8618
    public MainWorkspaceView()
    {
    }
#pragma warning restore CS8618

    public MainWorkspaceView(ILogger<MainWorkspaceView> p_logger,
        MainWorkspaceViewModel p_viewModel)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Creating MainWorkspaceView");

        InitializeComponent();

        DataContext = p_viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
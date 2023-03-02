using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using TransactionNavigator.ViewModels.MainApplication;

namespace TransactionNavigator.Views.MainApplication;

public partial class BalanceView : UserControl
{
    private readonly ILogger<BalanceView> m_logger;

#pragma warning disable CS8618
    public BalanceView()
    {
    }
#pragma warning restore CS8618

    public BalanceView(ILogger<BalanceView> p_logger,
        BalanceViewModel p_viewModel)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Creating BalanceView");

        InitializeComponent();

        DataContext = p_viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    [UsedImplicitly]
    private void InputElement_OnPointerReleased(object? p_sender, PointerReleasedEventArgs p_e)
    {
        base.OnPointerReleased(p_e);
        p_e.Handled = true;
    }

    [UsedImplicitly]
    private void ConfirmationButton_OnClick(object? p_sender, RoutedEventArgs p_e)
    {
        if (p_sender is not Button { Parent: StackPanel { Parent: Border { Parent: MenuItem { Parent: MenuItem { Parent: ContextMenu contextMenu } } } } }) return;

        contextMenu.Close();
    }
}
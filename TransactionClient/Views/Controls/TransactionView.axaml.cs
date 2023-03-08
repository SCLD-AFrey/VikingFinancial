using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using TransactionClient.ViewModels.Controls;

namespace TransactionClient.Views.Controls;

public partial class TransactionView : UserControl
{
    private readonly ILogger<TransactionView> m_logger;
#pragma warning disable CS8618
    public TransactionView()
    {
    }
#pragma warning restore CS8618
    public TransactionView(ILogger<TransactionView> p_logger,  TransactionViewModel p_viewModel)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Creating TransactionView");
        InitializeComponent();
        DataContext = p_viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
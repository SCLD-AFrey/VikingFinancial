using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using TransactionClient.ViewModels.Controls;

namespace TransactionClient.Views.Controls;

public partial class DetailsView : UserControl
{
    private readonly ILogger<DetailsView> m_logger;
    
    
#pragma warning disable CS8618
    public DetailsView()
    {
    }
#pragma warning restore CS8618
    public DetailsView(ILogger<DetailsView> p_logger, DetailsViewModel p_viewModel)
    {
        m_logger = p_logger;
        m_logger.LogDebug("Creating DetailsView");
        InitializeComponent();
        DataContext = p_viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
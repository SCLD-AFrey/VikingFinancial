using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Logging;
using VikingFinancial.Gui.ViewModels.MainApplication.MainWindow;

namespace VikingFinancial.Gui.Views.MainApplication.MainWindow;

public partial class LandingPageView : UserControl
{
    private readonly ILogger<LandingPageView> m_logger;

#pragma warning disable CS8618
    public LandingPageView()
    {
    }
#pragma warning restore CS8618

    public LandingPageView(ILogger<LandingPageView> p_logger,
        LandingPageViewModel p_viewModel)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Creating WelcomePageView");

        InitializeComponent();

        DataContext = p_viewModel;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
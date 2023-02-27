using Microsoft.Extensions.Logging;
using VikingFinancial.Gui.Models.Services;

namespace VikingFinancial.Gui.ViewModels.MainApplication.MainWindow;

public class LandingPageViewModel
{
    private readonly ILogger<LandingPageViewModel> m_logger;
    private readonly Files m_files;

    public LandingPageViewModel(ILogger<LandingPageViewModel> p_logger, Files p_files)
    {
        m_logger = p_logger;
        m_files = p_files;

        m_logger.LogDebug("Instantiating WelcomePageModel");
    }

    public void SetNavigation(string p_parameter)
    {
        m_logger.LogDebug("Requesting navigation to '{NavigationTarget:l}' from Navigation service", p_parameter);

    }

}
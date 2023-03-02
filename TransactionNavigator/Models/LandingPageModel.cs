using Microsoft.Extensions.Logging;
using TransactionNavigator.Models.Services;

namespace TransactionNavigator.Models;

public class LandingPageModel
{
    private readonly ILogger<LandingPageModel> m_logger;
    private readonly Navigation m_navigationService;
    private readonly Files m_files;
    
    public LandingPageModel(ILogger<LandingPageModel> p_logger, Navigation p_navigationService, Files p_files)
    {
        m_logger = p_logger;
        m_navigationService = p_navigationService;
        m_files = p_files;
        m_logger.LogDebug("Instantiating LandingPageModel");
    }

    public void SetNavigation(string p_parameter)
    {
        m_logger.LogDebug("Requesting navigation to '{NavigationTarget:l}' from Navigation service", p_parameter);

        m_navigationService.SetNavigation(p_parameter);
    }
}
using Microsoft.Extensions.Logging;
using TransactionNavigator.Models.Services;

namespace TransactionNavigator.Models;

public class MainWorkspaceModel
{
    private readonly ILogger<MainWorkspaceModel> m_logger;
    private readonly Navigation m_navigationService;

    public MainWorkspaceModel(ILogger<MainWorkspaceModel> p_logger, Navigation p_navigationService)
    {
        m_logger = p_logger;
        m_navigationService = p_navigationService;

        m_logger.LogDebug("Instantiating MainWorkspaceModel");
    }
    
    public void SetNavigation(string p_parameter)
    {
        m_logger.LogDebug("Requesting navigation to '{NavigationTarget:l}' from Navigation service", p_parameter);
        
        m_navigationService.SetNavigation(p_parameter);
    }
}
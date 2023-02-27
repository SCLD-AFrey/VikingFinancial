using Microsoft.Extensions.Logging;
using VikingFinancial.Gui.Models.Services;

namespace VikingFinancial.Gui.Models.BackingModels.MainApplication;

public class MainWorkspaceModel
{
    private readonly ILogger<MainWorkspaceModel> m_logger;

    public MainWorkspaceModel(ILogger<MainWorkspaceModel> p_logger)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating MainWorkspaceModel");
    }
    
    public void SetNavigation(string p_parameter)
    {
        m_logger.LogDebug("Requesting navigation to '{NavigationTarget:l}' from Navigation service", p_parameter);
        
    }
}
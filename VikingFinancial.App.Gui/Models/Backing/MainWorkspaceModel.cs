using Microsoft.Extensions.Logging;

namespace VikingFinancial.App.Gui.Models.Backing;

public class MainWorkspaceModel
{
    private readonly ILogger<MainWorkspaceModel> m_logger;

    public MainWorkspaceModel(ILogger<MainWorkspaceModel> p_logger)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating MainWorkspaceModel");
    }
}
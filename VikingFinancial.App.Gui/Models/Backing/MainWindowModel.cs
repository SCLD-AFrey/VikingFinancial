using Microsoft.Extensions.Logging;

namespace VikingFinancial.App.Gui.Models.Backing;

public class MainWindowModel
{
    private readonly ILogger<MainWindowModel> m_logger;

    public MainWindowModel(ILogger<MainWindowModel> p_logger)
    {
        m_logger = p_logger;

        m_logger.LogDebug("Instantiating MainWindowModel");
    }
}
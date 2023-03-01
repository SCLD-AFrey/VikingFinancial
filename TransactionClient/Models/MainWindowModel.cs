using Microsoft.Extensions.Logging;

namespace TransactionClient.Models;
public class MainWindowModel
{
    private readonly ILogger<MainWindowModel> m_logger;
    public MainWindowModel(ILogger<MainWindowModel> p_logger)
    {
        m_logger                 = p_logger;
    }

}
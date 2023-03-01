using Microsoft.Extensions.Logging;
using VikingFinancial.TransactionServer.Services.Database;

namespace VikingFinancial.TransactionServer.Services;

public class ConfigurationService : ValkyrieFinancial.Protos.Configuration.ConfigurationService.ConfigurationServiceBase
{
    private readonly ILogger<ConfigurationService> m_logger;
    private readonly TransactionDatabaseInterface m_dbInterface;
    public ConfigurationService(ILogger<ConfigurationService> p_logger,
        TransactionDatabaseInterface p_dbInterface)
    {
        m_logger = p_logger;
        m_dbInterface = p_dbInterface;
    }
}


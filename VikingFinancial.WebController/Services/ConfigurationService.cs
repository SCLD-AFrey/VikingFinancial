using DevExpress.Xpo;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ValkyrieFinancial.Protos.Configuration;
using VikingFinancial.Data.Transaction;
using VikingFinancial.WebController.Services.Database;

namespace VikingFinancial.WebController.Services;

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


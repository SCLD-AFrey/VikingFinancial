using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Microsoft.Extensions.Logging;
using VikingFinancial.Data.Transaction;


// ReSharper disable UnusedVariable

namespace VikingFinancial.Gui.Models.Services.Database;

public class TransactionDatabaseInitialization
{
    private readonly ILogger<TransactionDatabaseInitialization> m_logger;
    private readonly TransactionDatabaseInterface m_dbInterface;
    private readonly Files m_requiredFilesService;

    public TransactionDatabaseInitialization(ILogger<TransactionDatabaseInitialization> p_logger,
        TransactionDatabaseInterface p_dbInterface, Files p_requiredFilesService)
    {
        m_logger = p_logger;
        m_dbInterface = p_dbInterface;
        m_requiredFilesService = p_requiredFilesService;
    }

    public bool IsFirstRun { get; set; }

    public async Task DoFirstTimeSetup()
    {
        m_logger.LogDebug("Checking if database data exists");

        using var unitOfWork = m_dbInterface.ProvisionUnitOfWork();

        if (File.Exists(m_requiredFilesService.DatabaseFilePath))
        {
            var fileInfo = new FileInfo(m_requiredFilesService.DatabaseFilePath);

            if (fileInfo.Length == 0)
            {
                IsFirstRun = true;

                m_logger.LogDebug("Database does not exist, creating initial groups");

                var adminUser = new UserProfile(unitOfWork)
                {

                };

                //Add categories
                

                await unitOfWork.CommitChangesAsync();

                return;
            }

            //var forgeInfo = unitOfWork.Query<ForgeData>();

            //if (forgeInfo.Any())
            //{
            //    CheckForgeVersion(forgeInfo);
            //    return;
            //}

            IsFirstRun = true;
        }
        else
        {
            throw new DataException("Database file does not exist but should at this point in program execution");
        }
    }


}
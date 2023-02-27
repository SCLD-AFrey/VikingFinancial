using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Microsoft.Extensions.Logging;
using VikingFinancial.Data.Transaction;


// ReSharper disable UnusedVariable

namespace VikingFinancial.App.Gui.Models.Services.Database;

public class TransDatabaseInitialization
{
    private readonly ILogger<TransDatabaseInitialization> m_logger;
    private readonly TransDatabaseInterface m_dbInterface;
    private readonly Files m_requiredFilesService;
    private readonly Crypto m_cryptoService;

    public TransDatabaseInitialization(ILogger<TransDatabaseInitialization> p_logger,
        Crypto p_cryptoService,
        TransDatabaseInterface p_dbInterface, 
        Files p_requiredFilesService)
    {
        m_logger = p_logger;
        m_dbInterface = p_dbInterface;
        m_requiredFilesService = p_requiredFilesService;
        m_cryptoService = p_cryptoService;
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


                try
                {
                    //Debit Categories
                    var catFood = new DebitCategory(unitOfWork) { Title = "Food", IsLocked = false };
                    var catEntertainment = new DebitCategory(unitOfWork) { Title = "Entertainment", IsLocked = false };
                    var catTransportation = new DebitCategory(unitOfWork) { Title = "Transportation", IsLocked = false };
                    var catUtilities = new DebitCategory(unitOfWork) { Title = "Utilities", IsLocked = false };
                    var catOther = new DebitCategory(unitOfWork) { Title = "Other", IsLocked = true };
                    var catUnknown = new DebitCategory(unitOfWork) { Title = "Unknown", IsLocked = true };
                    
                    //init budgets
                    var budgetFood = new BudgetItem(unitOfWork) { Category = catFood, Amount = 0 };
                    var budgetEntertainment = new BudgetItem(unitOfWork) { Category = catEntertainment, Amount = 0 };
                    var budgetTransportation = new BudgetItem(unitOfWork) { Category = catTransportation, Amount = 0 };
                    var budgetUtilities = new BudgetItem(unitOfWork) { Category = catUtilities, Amount = 0 };
                    
                    //Credit Categories
                    var catDSteelCloud = new CreditCategory(unitOfWork) { Title = "SteelCloud", IsLocked = false };
                    var catDBentRiver = new CreditCategory(unitOfWork) { Title = "Bent River", IsLocked = false };
                    var catDOther = new CreditCategory(unitOfWork) { Title = "Other", IsLocked = true };
                    var catDUnknown = new CreditCategory(unitOfWork) { Title = "Unknown", IsLocked = true };
                    var catDInitial= new CreditCategory(unitOfWork) { Title = "Initial Deposit", IsLocked = true };

                    var adminUser = new UserProfile(unitOfWork)
                    {
                        Username = "admin",
                        Password = m_cryptoService.GeneratePasswordHash("password", out var salt),
                        Salt = salt,
                        IsAdmin = true
                    };

                    var initDeposit = new Credit(unitOfWork)
                    {
                        Amount = 0, Category = catDInitial, DateTransaction = DateTime.UtcNow, Desc = "Initial Deposit",
                        AddedByUser = adminUser
                    };
                    await unitOfWork.CommitChangesAsync();
                } catch(Exception e)
                {
                    m_logger.LogError(e, "Error creating initial groups");
                }
            }

            IsFirstRun = true;
        }
        else
        {
            throw new DataException("Database file does not exist but should at this point in program execution");
        }
    }
}
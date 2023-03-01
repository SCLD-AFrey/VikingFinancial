using System.Data;
using Microsoft.Extensions.Logging;
using VikingFinancial.Data.Transaction;

namespace VikingFinancial.TransactionServer.Services.Database
{
    public class TransactionDatabaseInit
    {
        private readonly ILogger<TransactionDatabaseInit> m_logger;
        private readonly Crypto m_crypto;
        private readonly TransactionDatabaseInterface m_dbInterface;
        private readonly Files m_requiredFilesService;

        public TransactionDatabaseInit(ILogger<TransactionDatabaseInit> p_logger,
            TransactionDatabaseInterface p_dbInterface, Files p_requiredFilesService, Crypto p_crypto)
        {
            m_logger = p_logger;
            m_dbInterface = p_dbInterface;
            m_requiredFilesService = p_requiredFilesService;
            m_crypto = p_crypto;
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

                    //Add init
                    var newAdminUser = new UserProfile(unitOfWork)
                    {
                        Username = "Admin",
                        Password = m_crypto.GeneratePasswordHash("password", out var salt),
                        Salt = salt,
                        IsAdmin = true,
                        IsLocked = true
                    };
                    
                    //Add Credit Cats
                    var creditCat1 = new CreditCategory(unitOfWork) { Title = "SteelCloud" };
                    var creditCat2 = new CreditCategory(unitOfWork) { Title = "Herbal" };
                    var creditCat3 = new CreditCategory(unitOfWork) { Title = "Initial", IsLocked = true};
                    
                    //Add Debit Cats
                    var debitCat1 = new DebitCategory(unitOfWork) { Title = "Mortgage" };
                    var debitCat2 = new DebitCategory(unitOfWork) { Title = "Utilities" };
                    var debitCat3 = new DebitCategory(unitOfWork) { Title = "Gas/Transportation" };
                    var debitCat4 = new DebitCategory(unitOfWork) { Title = "Entertainment" };
                    var debitCat5 = new DebitCategory(unitOfWork) { Title = "Food" };
                    
                    var transType1 = new TransactionType(unitOfWork) { Title = "Cash" };
                    var transType2 = new TransactionType(unitOfWork) { Title = "Check" };
                    var transType3 = new TransactionType(unitOfWork) { Title = "Debit Card" };

                    await unitOfWork.CommitChangesAsync();

                    return;
                }


                IsFirstRun = true;
            }
            else
            {
                throw new DataException("Database file does not exist but should at this point in program execution");
            }
        }
    }
}

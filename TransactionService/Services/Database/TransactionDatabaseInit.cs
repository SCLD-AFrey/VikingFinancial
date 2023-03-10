using System.Data;
using DevExpress.Xpo;
using TransactionService.Services.Global;
using VikingFinancial.Common;
using VikingFinancial.Data.Transaction;

namespace TransactionService.Services.Database
{
    public class TransactionDatabaseInit
    {
        private readonly ILogger<TransactionDatabaseInit> m_logger;
        private readonly PasswordCrypto m_crypto;
        private readonly TransactionDatabaseInterface m_dbInterface;
        private readonly CommonFiles m_requiredFilesService;

        public TransactionDatabaseInit(ILogger<TransactionDatabaseInit> p_logger,
            TransactionDatabaseInterface p_dbInterface, CommonFiles p_requiredFilesService, PasswordCrypto p_crypto)
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

            if (File.Exists(m_requiredFilesService.DataBasePath))
            {
                var fileInfo = new FileInfo(m_requiredFilesService.DataBasePath);

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
                    var cat1 = new TransactionCategory(unitOfWork) { IsLocked = false, IsCredit = true, IsDebit = false, Title = "SteelCloud"};
                    var cat2 = new TransactionCategory(unitOfWork) { IsLocked = false, IsCredit = true, IsDebit = false, Title = "Valkyrie Springs"};
                    var cat3 = new TransactionCategory(unitOfWork) { IsLocked = true, IsCredit = true, IsDebit = false, Title = "Initial"};
                    var cat4 = new TransactionCategory(unitOfWork) { IsLocked = true, IsCredit = true, IsDebit = true, Title = "Miscellaneous"};
                    var cat5 = new TransactionCategory(unitOfWork) { IsLocked = false, IsCredit = false, IsDebit = true, Title = "Mortgage"};
                    var cat6 = new TransactionCategory(unitOfWork) { IsLocked = false, IsCredit = false, IsDebit = true, Title = "Utilities"};
                    var cat7 = new TransactionCategory(unitOfWork) { IsLocked = false, IsCredit = false, IsDebit = true, Title = "Gas/Transportation"};
                    var cat8 = new TransactionCategory(unitOfWork) { IsLocked = false, IsCredit = false, IsDebit = true, Title = "Entertainment"};
                    var cat9 = new TransactionCategory(unitOfWork) { IsLocked = false, IsCredit = false, IsDebit = true, Title = "Food"};
                    var cat0 = new TransactionCategory(unitOfWork) { IsLocked = true, IsCredit = true, IsDebit = true, Title = "Adjustment"};
                    
                    var transType1 = new TransactionType(unitOfWork) { Title = "Cash" };
                    var transType2 = new TransactionType(unitOfWork) { Title = "Check" };
                    var transType3 = new TransactionType(unitOfWork) { Title = "E-Deposit" };
                    var transType4 = new TransactionType(unitOfWork) { Title = "Debit Card" };
                    
                    await unitOfWork.CommitChangesAsync();

                    XPCollection<Transaction> transactions = new XPCollection<Transaction>(unitOfWork);

                    var adminUser = unitOfWork.Query<UserProfile>().FirstOrDefault(p_x => p_x.Username == "Admin");

                    for (var i = 0; i < 50; i++)
                    {
                        var amt = GenerateRandomDollarAmount();
                        if (i % 5 == 0)
                        {
                            amt *= -1;
                        }
                        transactions.Add(new Transaction(unitOfWork)
                        {
                            AddedByUser = adminUser,
                            Amount = amt,
                            Category = cat1,
                            Type = transType1,
                            Note = GenerateRandomSentence(), 
                            DateTransaction = GenerateRandomDate(DateTime.UtcNow - TimeSpan.FromDays(30), DateTime.UtcNow), 
                            DateCreated = GenerateRandomDate(DateTime.UtcNow - TimeSpan.FromDays(7), DateTime.UtcNow - TimeSpan.FromDays(1))
                        });  
                    }


                        decimal GenerateRandomDollarAmount()
                        {
                            Random random = new Random();
                            decimal amount = random.Next(10000) / 100.0m;
                            return amount;
                        }
                        DateTime GenerateRandomDate(DateTime startDate, DateTime endDate)
                        {
                            Random random = new Random();
                            int range = (endDate - startDate).Days;
                            return startDate.AddDays(random.Next(range));
                        }
                        string GenerateRandomSentence()
                        {
                            Random random = new Random();
                            string[] subjects = {"I", "You", "He", "She", "They", "We"};
                            string[] verbs = {"am", "are", "is", "was", "were", "have been", "has been"};
                            string[] objects = {"happy", "sad", "excited", "tired", "angry", "bored", "confused", "surprised"};
                            string subject = subjects[random.Next(subjects.Length)];
                            string verb = verbs[random.Next(verbs.Length)];
                            string obj = objects[random.Next(objects.Length)];
                            return $"{subject} {verb} {obj}.";
                        }

                    
                    

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

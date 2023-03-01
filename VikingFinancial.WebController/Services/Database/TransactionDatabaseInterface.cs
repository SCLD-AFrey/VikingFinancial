using DevExpress.Xpo;

namespace VikingFinancial.WebController.Services.Database;

public class TransactionDatabaseInterface : IDatabaseInterface
{
    public TransactionDatabaseInterface(DatabaseUtilities p_utilities)
    {
        var DatabaseDirectoryPath = @"C:\ProgramData\.VikingFinancialApp\db";
        DataLayer = p_utilities.GetDataLayer(DatabaseDirectoryPath, "transaction.db");
    }
    
    public IDataLayer DataLayer     { get; }
    
    public UnitOfWork ProvisionUnitOfWork()
    {
        return new UnitOfWork(DataLayer);
    }
}
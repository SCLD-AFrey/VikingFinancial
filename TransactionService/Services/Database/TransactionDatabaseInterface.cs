using DevExpress.Xpo;
using TransactionService.Services.Global;

namespace TransactionService.Services.Database;

public class TransactionDatabaseInterface : IDatabaseInterface
{
    public TransactionDatabaseInterface(DatabaseUtilities p_utilities, 
        CommonFiles p_requiredFolders)
    {
        DataLayer = p_utilities.GetDataLayer(p_requiredFolders.DataBasePath);
    }
    
    public IDataLayer DataLayer     { get; }
    
    public UnitOfWork ProvisionUnitOfWork()
    {
        return new UnitOfWork(DataLayer);
    }
}
using DevExpress.Xpo;

namespace VikingFinancial.App.Gui.Models.Services.Database;

public class TransDatabaseInterface : IDatabaseInterface
{
    public TransDatabaseInterface(DatabaseUtilities p_utilities, 
                                   Folders p_requiredFolders)
    {
        DataLayer = p_utilities.GetDataLayer(p_requiredFolders.DatabaseDirectoryPath, "transaction.db");
    }
    
    public IDataLayer DataLayer     { get; }
    
    public UnitOfWork ProvisionUnitOfWork()
    {
        return new UnitOfWork(DataLayer);
    }
}
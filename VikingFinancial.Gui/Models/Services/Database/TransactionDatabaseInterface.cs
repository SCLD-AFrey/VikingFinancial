using DevExpress.Xpo;

namespace VikingFinancial.Gui.Models.Services.Database;

public class TransactionDatabaseInterface : IDatabaseInterface
{
    public TransactionDatabaseInterface(DatabaseUtilities p_utilities,
        Folders p_requiredFolders)
    {
        DataLayer = p_utilities.GetDataLayer(p_requiredFolders.DatabaseDirectoryPath, "transaction.db");
    }

    public IDataLayer DataLayer { get; }

    public UnitOfWork ProvisionUnitOfWork()
    {
        return new UnitOfWork(DataLayer);
    }
}
using DevExpress.Xpo;

namespace VikingFinancial.App.Gui.Models.Services.Database;

public class DocumentDatabaseInterface
{
    public DocumentDatabaseInterface(DatabaseUtilities p_utilities, 
                                     Folders           p_requiredFolders)
    {
        DataLayer = p_utilities.GetDataLayer(p_requiredFolders.DatabaseDirectoryPath, "document.db");
    }
    
    public IDataLayer DataLayer { get; }
    
    public UnitOfWork ProvisionUnitOfWork()
    {
        return new UnitOfWork(DataLayer);
    }
}
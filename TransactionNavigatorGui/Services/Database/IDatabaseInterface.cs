using DevExpress.Xpo;

namespace TransactionNavigatorGui.Services.Database;

public interface IDatabaseInterface
{
    public IDataLayer DataLayer { get; }

    public UnitOfWork ProvisionUnitOfWork();
}
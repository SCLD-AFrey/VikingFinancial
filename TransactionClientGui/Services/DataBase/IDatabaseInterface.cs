using DevExpress.Xpo;

namespace TransactionClientGui.Services.DataBase;

public interface IDatabaseInterface
{
    public IDataLayer DataLayer { get; }

    public UnitOfWork ProvisionUnitOfWork();
}
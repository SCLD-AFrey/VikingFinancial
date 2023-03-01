// 

using DevExpress.Xpo;

namespace VikingFinancial.TransactionServer.Services.Database;

public interface IDatabaseInterface
{
    public IDataLayer DataLayer { get; }

    public UnitOfWork ProvisionUnitOfWork();
}
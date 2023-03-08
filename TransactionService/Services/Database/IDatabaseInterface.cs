// 

using DevExpress.Xpo;

namespace TransactionService.Services.Database;

public interface IDatabaseInterface
{
    public IDataLayer DataLayer { get; }

    public UnitOfWork ProvisionUnitOfWork();
}
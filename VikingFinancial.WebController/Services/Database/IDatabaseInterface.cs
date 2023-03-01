// 

using DevExpress.Xpo;

namespace VikingFinancial.WebController.Services.Database;

public interface IDatabaseInterface
{
    public IDataLayer DataLayer { get; }

    public UnitOfWork ProvisionUnitOfWork();
}
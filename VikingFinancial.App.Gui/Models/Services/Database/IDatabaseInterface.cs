// 

using DevExpress.Xpo;

namespace VikingFinancial.App.Gui.Models.Services.Database;

public interface IDatabaseInterface
{
    public IDataLayer DataLayer { get; }

    public UnitOfWork ProvisionUnitOfWork();
}
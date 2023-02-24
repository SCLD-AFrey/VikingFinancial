using DevExpress.Xpo;

namespace VikingFinancial.Gui.Models.Services.Database;

public interface IDatabaseInterface
{
    public IDataLayer DataLayer { get; }

    public UnitOfWork ProvisionUnitOfWork();
}
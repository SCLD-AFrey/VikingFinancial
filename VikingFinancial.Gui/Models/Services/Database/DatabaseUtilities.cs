using System.IO;
using System.Security;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace VikingFinancial.Gui.Models.Services.Database;

public class DatabaseUtilities
{
    public IDataLayer GetDataLayer(string p_databaseLocation,
        string p_databaseName,
        SecureString? p_databasePassword = null)
    {
        var connectionString = p_databasePassword switch
        {
            null => SQLiteConnectionProvider.GetConnectionString(Path.Combine(p_databaseLocation,
                p_databaseName)),
            _ => SQLiteConnectionProvider.GetConnectionString(
                Path.Combine(p_databaseLocation, p_databaseName))
        };

        return new SimpleDataLayer(XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema));
    }
}
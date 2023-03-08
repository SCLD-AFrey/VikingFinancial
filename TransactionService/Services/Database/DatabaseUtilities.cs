// 

using System.Security;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace TransactionService.Services.Database;

public class DatabaseUtilities
{
    public DatabaseUtilities() { }
    public IDataLayer GetDataLayer(string p_databaseLocation)
    {
        var connectionString = SQLiteConnectionProvider.GetConnectionString(p_databaseLocation);
        return new SimpleDataLayer(XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema));
    }
}
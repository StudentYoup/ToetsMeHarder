using Microsoft.Data.SqlClient;

namespace ToetsMeHarder.Business;

public interface IDataManager
{
    public SqlConnection Connection { get; }
    public void Connect();
    public void Disconnect();
}
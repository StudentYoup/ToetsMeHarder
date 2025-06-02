using MySqlConnector;

namespace ToetsMeHarder.Business;

public interface IDataManager
{
    public MySqlConnection Connection { get; }
    public void Connect();
    public void Disconnect();
    public void SetResult(Result r);
}
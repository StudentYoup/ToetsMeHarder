using Microsoft.Data.SqlClient;
using ToetsMeHarder.Business;

namespace ToetsMeHarder.DataAccess;

public class DataManager : IDataManager
{
    public SqlConnection Connection { get; private set;}

    public async void Connect()
    {
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = "192.168.1.10",
            UserID = "ToetsMeHarder",
            Password = "Welkom01!",
            InitialCatalog = "ToetsMeHarder",
        };
        
        string connectionString = builder.ConnectionString;

        try
        {
            await using SqlConnection connection = new SqlConnection(connectionString);
            Connection = connection;
            await Connection.OpenAsync();
        }
        catch (SqlException e)
        {
            Console.WriteLine($"SQL ERROR: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public async void Disconnect()
    {
        await Connection.CloseAsync();
    }
}
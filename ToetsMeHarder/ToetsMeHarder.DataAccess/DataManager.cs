using MySqlConnector;
using ToetsMeHarder.Business;

namespace ToetsMeHarder.DataAccess;

public class DataManager : IDataManager
{
    public MySqlConnection Connection { get; private set;}

    public void Connect()
    {
        try
        {
            Connection = new MySqlConnection("server=192.168.1.10;database=ToetsMeHarder;user=ToetsMeHarder;password=Welkom01!");
            Connection.Open();
        }
        catch (MySqlException e)
        {
            Console.WriteLine($"SQL ERROR: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void Disconnect()
    {
         Connection.Close();
    }

    public async void SetResult(Result r)
    {
        if (Connection == null) return;
        
        try
        {
            string queery = $"INSERT INTO toetsmeharder.result (`Username`, `Song`, `Accuracy`, `Speed`, `Total`) VALUES ({r.Username},{r.SongID},{r.Accuracy},{r.Speed},{r.Total});";
            await using var command = new MySqlCommand(queery, Connection);
        }
        catch (MySqlException e)
        {
            Console.WriteLine($"SQL ERROR: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
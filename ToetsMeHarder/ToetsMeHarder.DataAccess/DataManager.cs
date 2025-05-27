
﻿using MySqlConnector;
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

    public Result GetResult(int SongiD)
    {
        try
        {
            using var command = new MySqlCommand($"Select Title, Username, Song, Accuracy, Speed, Total From result Join song on ID = Song Where Song ={SongiD}", Connection);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Result
                {
                    Username = reader.GetString("Username"),    // gebruik kolomnamen!
                    SongID = reader.GetInt32("Song"),
                    Accuracy = reader.GetInt32("Accuracy"),
                    Speed = reader.GetInt32("Speed"),
                    Total = reader.GetInt32("Total"),
                    SongTitle = reader.GetString("Title")
                };
            }
        }
        catch (MySqlException e)
        {
            Console.WriteLine($"SQL ERROR: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        return null;
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
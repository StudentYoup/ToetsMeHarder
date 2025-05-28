using System.Data;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal.Commands;
using ToetsMeHarder.Business;
using ToetsMeHarder.DataAccess;

namespace ToetsMeHarder.Tests;

public class DataManagerTest
{
    private DataManager _dataManager;

    [SetUp]
    public void Setup()
    {
        _dataManager = new DataManager();
        _dataManager.Connect();
    }
    
    [Test]
    public void CONNECT_DATAMANAGER_SUCCESSFULLCONNECT()
    {
        TimeSpan timeout = TimeSpan.FromSeconds(40);
        DateTime start = DateTime.Now;
        while (_dataManager.Connection.State.ToString() != "Open" && DateTime.Now - start < timeout)
        {
            Thread.Sleep(100);
        }
        
        Assert.That(_dataManager.Connection.State.ToString(), Is.EqualTo("Open"));
    }
    
}
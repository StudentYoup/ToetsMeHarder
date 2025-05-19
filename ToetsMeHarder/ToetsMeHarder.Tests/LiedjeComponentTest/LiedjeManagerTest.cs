using System.ComponentModel;
using Moq;
using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.Tests.LiedjeComponentTest;

[TestFixture]
public class LiedjeManagerTest
{
    [Test]
    public void ONPROPERTYCHANGED_FIRES_EVENT()
    {
        bool eventFired = false;
        SongManager.Instance.RegisterPropertyChangedFunction((object? sender,PropertyChangedEventArgs args)=>eventFired = true);
        SongManager.Instance.GekozenLiedje = new Song("BOB", 120,2000,"C",5);
        Assert.That(eventFired, Is.True);
    }

}
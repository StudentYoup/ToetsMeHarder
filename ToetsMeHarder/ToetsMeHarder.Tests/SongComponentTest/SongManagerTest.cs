using System.ComponentModel;
using Moq;
using ToetsMeHarder.Business.SongsComponent;

namespace ToetsMeHarder.Tests.SongsComponent;

[TestFixture]
public class SongManagerTest
{
    [Test]
    public void ONPROPERTYCHANGED_FIRES_EVENT()
    {
        bool eventFired = false;
        SongsManager.Instance.RegisterPropertyChangedFunction((object? sender,PropertyChangedEventArgs args)=>eventFired = true);
        SongsManager.Instance.ChosenSong = new Songs("BOB", 120,1000,"A");
        Assert.That(eventFired, Is.True);
    }

}
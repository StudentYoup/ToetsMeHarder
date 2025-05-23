using System.ComponentModel;
using Moq;
using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.Tests.LiedjeComponentTest;

[TestFixture]
public class SongManagerTest
{
    [Test]
    public void ONPROPERTYCHANGED_FIRES_EVENT()
    {
        bool eventFired = false;
        SongsManager.Instance.RegisterPropertyChangedFunction((object? sender,PropertyChangedEventArgs args)=>eventFired = true);
        SongsManager.Instance.ChosenSong = new Songs("BOB", 120);
        Assert.That(eventFired, Is.True);
    }

}
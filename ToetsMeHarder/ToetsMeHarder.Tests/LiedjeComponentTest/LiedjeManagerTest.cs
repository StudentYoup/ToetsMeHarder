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
        LiedjesManager.Instance.RegisterPropertyChangedFunction((object? sender,PropertyChangedEventArgs args)=>eventFired = true);
        LiedjesManager.Instance.GekozenLiedje = new Liedje("BOB", 120);
        Assert.That(eventFired, Is.True);
    }

}
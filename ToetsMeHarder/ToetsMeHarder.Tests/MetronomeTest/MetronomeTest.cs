using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Threading.Tasks;
using ToetsMeHarder.PianoGUI.Business;

namespace ToetsMeHarder.Tests
{
    [TestFixture]
    public class MetronomeServiceTests
    {
        private MetronomeService _metronome;

        [SetUp]
        public void Setup()
        {
            _metronome = new MetronomeService(); // test constructor want mocking werkte niet
        }

        [Test]
        public void Default_BPM_ShouldBe_60()
        {
            Assert.That(_metronome.BPM, Is.EqualTo(60));
        }

        [Test]
        public void Can_Start_And_Stop()
        {
            _metronome.Start();
            ClassicAssert.IsTrue(_metronome.IsRunning);

            _metronome.Stop();
            ClassicAssert.IsFalse(_metronome.IsRunning);
        }

        [Test]
        public void BPM_Cannot_Be_Less_Than_20()
        {
            _metronome.BPM = 5;
            Assert.That(_metronome.BPM, Is.EqualTo(20));
        }

        [Test]
        public void BPM_Cannot_Be_More_Than_300()
        {
            _metronome.BPM = 400;
            Assert.That(_metronome.BPM, Is.EqualTo(300));
        }

        [Test]
        public async Task Beat_Event_Fires_When_Running()
        {
            bool beatFired = false;
            _metronome.Beat += (s, e) => beatFired = true;

            _metronome.BPM = 240;
            _metronome.Start();

            await Task.Delay(260); //iets langer wachten dan de tijd van 1 beat

            _metronome.Stop();

            ClassicAssert.IsTrue(beatFired);
        }
    }
}

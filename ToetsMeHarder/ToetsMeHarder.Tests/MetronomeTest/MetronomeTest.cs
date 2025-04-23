using NUnit.Framework;
using Moq;
using System;
using System.Threading.Tasks;
using ToetsMeHarder.PianoGUI.Business;
using Plugin.Maui.Audio;

namespace ToetsMeHarder.Tests
{
    [TestFixture]
    public class MetronomeTests
    {
        private Mock<IMetronomeService> _metronomeMock;
        private Mock<IAudioManager> _audioManagerMock; 


        [SetUp]
        public void Setup()
        {
            _metronomeMock = new Mock<IMetronomeService>();
        }

        [Test]
        public void Default_BPM_ShouldBe_60()
        {
            _metronomeMock.SetupProperty(m => m.BPM, 60);

            Assert.That(_metronomeMock.Object.BPM, Is.EqualTo(60));
        }

        [Test]
        public void Can_Start_And_Stop()
        {
            _metronomeMock.Setup(m => m.IsRunning).Returns(true);

            _metronomeMock.Object.Start();
            Assert.That(_metronomeMock.Object.IsRunning, Is.True);

            _metronomeMock.Setup(m => m.IsRunning).Returns(false);
            _metronomeMock.Object.Stop();
            Assert.That(_metronomeMock.Object.IsRunning, Is.False);
        }

        [Test]
        public void BPM_Cannot_Be_Less_Than_20()
        {
            _metronomeMock.SetupProperty(m => m.BPM, 60);

            _metronomeMock.Object.BPM = 5;
            _metronomeMock.SetupGet(m => m.BPM).Returns(20);

            Assert.That(_metronomeMock.Object.BPM, Is.GreaterThanOrEqualTo(20));
        }

        [Test]
        public void BPM_Cannot_Be_More_Than_300()
        {
            _metronomeMock.SetupProperty(m => m.BPM, 60);

            _metronomeMock.Object.BPM = 400;
            _metronomeMock.SetupGet(m => m.BPM).Returns(300);

            Assert.That(_metronomeMock.Object.BPM, Is.LessThanOrEqualTo(300));
        }

        [Test]
        public void Can_Set_BPM()
        {
            _metronomeMock.SetupProperty(m => m.BPM, 60);

            _metronomeMock.Object.BPM = 120;
            Assert.That(_metronomeMock.Object.BPM, Is.EqualTo(120));
        }

        
    }
}
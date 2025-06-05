using NUnit.Framework;
using NUnit.Framework.Legacy;
using ToetsMeHarder.Business.PianoComponent;
using ToetsMeHarder.Business.FallingBlocks;

namespace ToetsMeHarder.Tests.PianoManagerTest
{
    public class PianoManagerTests
    {
        private PianoManager _manager = new(new AudioHandler());

        [Test]
        public void ChangeKeyModus_ShouldSwitchBetweenKeymodes()
        {
            Assert.That(_manager._keyModus, Is.EqualTo(KeyModus.Key));
            _manager.ChangeKeyModus();
            Assert.That(_manager._keyModus, Is.EqualTo(KeyModus.Blank));
            _manager.ChangeKeyModus();
            Assert.That(_manager._keyModus, Is.EqualTo(KeyModus.Note));
        }

        [TestCase(KeyValue.c4, "c4")]
        [TestCase(KeyValue.c41, "c4#")]
        [TestCase(KeyValue.d5, "d5")]
        [TestCase(KeyValue.d51, "d5#")]
        [TestCase(KeyValue.a2, "a2")]
        [TestCase(KeyValue.a21, "a2#")]
        [TestCase(KeyValue.f5, "f5")]
        [TestCase(KeyValue.f51, "f5#")]
        public void GetKeyName_ShouldReturnStringNoteName(KeyValue key, string expected)
        {
            string result = _manager.GetKeyName(key);
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}

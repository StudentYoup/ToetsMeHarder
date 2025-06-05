using NUnit.Framework;
using NUnit.Framework.Legacy;
using ToetsMeHarder.Business.PianoComponent;
{
    
}

namespace ToetsMeHarder.Tests.PianoManagerTest
{
    public class PianoManagerTests
    {
        private PianoManager _manager = new();

        [Test]
        public void ChangeKeyModus_ShouldSwitchBetweenKeymodes()
        {
            Assert.That(_manager._keyModus, Is.EqualTo(KeyModus.Key));
            _manager.ChangeKeyModus();
            Assert.That(_manager._keyModus, Is.EqualTo(KeyModus.Blank));
            _manager.ChangeKeyModus();
            Assert.That(_manager._keyModus, Is.EqualTo(KeyModus.Note));
        }

        [TestCase(Business.FallingBlocks.KeyValue.c4, "c4")]
        [TestCase(Business.FallingBlocks.KeyValue.c41, "c4#")]
        [TestCase(Business.FallingBlocks.KeyValue.d5, "d5")]
        [TestCase(Business.FallingBlocks.KeyValue.d51, "d5#")]
        [TestCase(Business.FallingBlocks.KeyValue.a2, "a2")]
        [TestCase(Business.FallingBlocks.KeyValue.a21, "a2#")]
        [TestCase(Business.FallingBlocks.KeyValue.f5, "f5")]
        [TestCase(Business.FallingBlocks.KeyValue.f51, "f5#")]
        public void GetKeyName_ShouldReturnStringNoteName(Business.FallingBlocks.KeyValue key, string expected)
        {
            string result = _manager.GetKeyName(key);
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}

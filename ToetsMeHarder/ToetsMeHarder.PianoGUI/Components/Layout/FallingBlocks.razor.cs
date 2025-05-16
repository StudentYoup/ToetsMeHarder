using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class FallingBlocks
    {
        private Random _random = new();
        private Dictionary<int, List<int>> _blockMap = new();
        private int _numberOfBars = 24;
        protected override void OnInitialized()
        {
            base.OnInitialized();

            for (int i = 0; i < _numberOfBars; i++)
            {
                _blockMap[i] = new List<int> { 1 };
            }
            PeriodicBlockDrop();
        }

        /*make one new block per second*/
        private async Task PeriodicBlockDrop()
        {
            while (true)
            {
                int barIndex = _random.Next(0, _numberOfBars);
                _blockMap[barIndex].Add(_random.Next());
                StateHasChanged();
                await Task.Delay(1000); // timer part, now set to a minute(1000ms)
            }
        }
    }
}

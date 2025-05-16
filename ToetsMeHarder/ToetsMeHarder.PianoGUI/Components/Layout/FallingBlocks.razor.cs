using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToetsMeHarder.Business.FallingBlocks;
using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class FallingBlocks
    {
        private Song song;
        private List<NoteBlock> noteBlocks = new List<NoteBlock>();
        public List<string> GridDivs { get; set; } = new();
        
        public FallingBlocks()
        {
            GenerateDivs(40);
        }

        public FallingBlocks(int amount, Song song)
        {
            this.song = song;
            GenerateDivs(amount);
        }

        public List<string> GenerateDivs(int count)
        {
            List<string> divs = new List<string>();
            for (int i = 0; i < count; i++)
            {
                divs.Add($"<div class='Grid-block' style='width: {100 / count}%;'>test</div>"); //make the divs with a width based on the total amount(100/count = %%)
            }

            return GridDivs = divs;
        }
    }
}

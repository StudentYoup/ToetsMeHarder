using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.PianoGUI.Components.Layout
{
    public partial class FallingBlocks
    {
        private Song song;
        public List<string> GridDivs { get; set; } = new();
        
        public FallingBlocks()
        {
            GenerateGrid(400);
        }

        public FallingBlocks(int amount, Song song)
        {
            this.song = song;
            GenerateGrid(amount);
        }

        public List<string> GenerateGrid(int count)
        {
            List<string> divs = new List<string>();
            for (int i = 0; i < count; i++)
            {
                divs.Add($"<div class='Grid-block' style='width: {100 / count}%;'>test</div>"); //make the divs with a width based on the total amount(100/count = %%)
            }

            return GridDivs = divs;
        }

        public List<string> GenerateBlocks(Song song)
        {
            song.FillBlocks();
            List<string> comps = new List<string>();

            foreach (var b in song.blocks)
            {
                comps.Add($"<div class = 'falling-block'>{b.Key}</div>");
            }
            return comps;
        }


    }
}

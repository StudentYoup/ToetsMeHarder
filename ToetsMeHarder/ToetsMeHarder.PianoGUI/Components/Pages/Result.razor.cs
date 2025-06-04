

using ToetsMeHarder.PianoGUI.Components.Layout;
namespace ToetsMeHarder.PianoGUI.Components.Pages;

public partial class Result : Microsoft.AspNetCore.Components.ComponentBase
{
    private ToetsMeHarder.Business.Result _result => FallingBlocks.instance.fallingBlocksManager.CurrentResult;
    private int CalculateAccuracy()
    {
        if (_result.Hits == 0 && _result.Misses == 0)
            return 0;
        return Math.Max(0, (int)(((double)_result.Hits / (_result.Hits + _result.Misses)) * 100));

    }
}
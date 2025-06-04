using ToetsMeHarder.Business.ResultComponent;
using ToetsMeHarder.PianoGUI.Components.Layout;
namespace ToetsMeHarder.PianoGUI.Components.Pages;

public partial class Result
{
    public ResultManager resultManager = new ResultManager();
    protected override void OnInitialized()
    {
        resultManager._result = FallingBlocks.instance.fallingBlocksManager.CurrentResult;
    }
}
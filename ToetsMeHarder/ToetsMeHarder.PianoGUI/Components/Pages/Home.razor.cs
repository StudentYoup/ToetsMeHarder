using ToetsMeHarder.PianoGUI.Components.Layout;
using ToetsMeHarder.PianoGUI.Pages;
using ToetsMeHarder.Business;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace ToetsMeHarder.PianoGUI.Components.Pages
{
    public partial class Home
    {
        private ElementReference wrapper;
        private Piano? piano;

        private void OnKeyDown(KeyboardEventArgs e)
        {
            piano?.HandleKeyDown(e);  
        }
        private void OnKeyUp(KeyboardEventArgs e)
        {
            piano?.HandleKeyUp(e);  
        }
    }
}
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
        private bool _isFocused = false;
        private ElementReference wrapper;
        private Piano? piano;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !_isFocused)
            {
                _isFocused = true;
                await wrapper.FocusAsync(); // focus op de piano wrapper bij eerste render
            }
        }

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
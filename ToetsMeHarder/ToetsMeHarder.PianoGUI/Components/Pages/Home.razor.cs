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
        //PopUps:
        public bool helpPopUp = false;

        private bool _isFocused = false;
        private ElementReference _wrapper;
        private Piano? _piano;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !_isFocused)
            {
                _isFocused = true;
                await _wrapper.FocusAsync(); // focus op de piano wrapper bij eerste render
            }
        }

        private void OnKeyDown(KeyboardEventArgs e)
        {
            _piano?.HandleKeyDown(e);  
        }
        private void OnKeyUp(KeyboardEventArgs e)
        {
            _piano?.HandleKeyUp(e);  
        }

        private void OnFocusOut(){
            _piano.OnLostFocus();
        }
    }
}
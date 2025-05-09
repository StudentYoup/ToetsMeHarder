using ToetsMeHarder.PianoGUI.Components.Layout;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business.LiedjesComponent;
using PropertyChangedEventArgs = System.ComponentModel.PropertyChangedEventArgs;

namespace ToetsMeHarder.PianoGUI.Components.Pages
{
    public partial class Home
    {
        //PopUps:
        public bool helpPopUp = false;
        public bool resultPopUp = false;


        private bool _isFocused = false;
        private ElementReference _wrapper;
        private Piano? _piano;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            LiedjesManager.Instance.RegisterPropertyChangedFunction(OnliedjeChanged);
        }

        private void OnliedjeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LiedjesManager.Instance.GekozenLiedje))InvokeAsync(StateHasChanged);
        }

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
        private void Exit()
        {
            Application.Current.Quit(); 
        }
    }
}
using ToetsMeHarder.PianoGUI.Components.Layout;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business.SongsComponent;
using PropertyChangedEventArgs = System.ComponentModel.PropertyChangedEventArgs;

namespace ToetsMeHarder.PianoGUI.Components.Pages
{
    public partial class Home
    {
        public static Home Instance { get; private set; }

        //PopUps:
        private bool _helpPopUp = false;
        private bool _resultPopUp = false;
        public bool resultPopUp { get
            {
                return _resultPopUp;
            }
            set
            {
                _resultPopUp = value;
                StateHasChanged();
            }
        }

        public bool _songPopUp = false;

        private ElementReference _wrapper;
        private Piano? _piano;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SongsManager.Instance.RegisterPropertyChangedFunction(OnsongChanged);
            Home.Instance = this;
        }

        private void OnsongChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SongsManager.Instance.ChosenSong))InvokeAsync(StateHasChanged);
        }

        private void OnSongChanged()
        {
            _songPopUp = false;
            StateHasChanged(); // update UI
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
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
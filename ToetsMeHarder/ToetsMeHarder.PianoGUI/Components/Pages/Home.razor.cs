using ToetsMeHarder.PianoGUI.Components.Layout;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business.LiedjesComponent;
using PropertyChangedEventArgs = System.ComponentModel.PropertyChangedEventArgs;

namespace ToetsMeHarder.PianoGUI.Components.Pages
{
    public partial class Home
    {
        private bool _isFocused = false;
        private ElementReference _wrapper;
        private Piano? _piano;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SongManager.Instance.RegisterPropertyChangedFunction(OnliedjeChanged);
            
            // Randomly populate each bar with block IDs (just for demo)
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

        private void OnliedjeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SongManager.Instance.GekozenLiedje))InvokeAsync(StateHasChanged);
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
    }
}
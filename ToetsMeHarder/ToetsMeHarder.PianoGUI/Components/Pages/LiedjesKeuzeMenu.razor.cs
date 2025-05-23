using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.PianoGUI.Components.Pages;

public partial class LiedjesKeuzeMenu_razor : ComponentBase
{
    private LiedjesManager _songManager = LiedjesManager.Instance;
    Songs song = new Songs("TEST",120);
    
    [Parameter] public EventCallback OnOpen { get; set;}
    [Parameter] public EventCallback OnClose { get; set;}
    [Parameter] public bool IsOpen { get; set;} = false;
    [Parameter] public EventCallback SongChosen { get; set;}
    public async Task SetSong()
    {
        _songManager.ChosenSong = song;
        await SongChosen.InvokeAsync(); // Notify parent to close pop-up
    }

    public async void Open()
    {
        await OnOpen.InvokeAsync();
        IsOpen = true;
        StateHasChanged();
    }

    public async void Close()
    {
        await OnClose.InvokeAsync();
        IsOpen = false;
    }
}
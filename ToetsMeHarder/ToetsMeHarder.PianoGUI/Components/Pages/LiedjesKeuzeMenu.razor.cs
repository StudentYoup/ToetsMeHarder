using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.PianoGUI.Components.Pages;

public partial class LiedjesKeuzeMenu_razor : ComponentBase
{
    private bool _liedjeGekozen = false;

    private LiedjesManager _liedjesManager = LiedjesManager.Instance;
    Liedje lied = new Liedje("TEST",120);
    
    [Parameter] public EventCallback OnOpen { get; set;}
    [Parameter] public EventCallback OnClose { get; set;}
    [Parameter] public bool IsOpen { get; set; } = false;
    
    [Parameter] public EventCallback LiedjeGekozen { get; set; }

    public async Task SetLiedje()
    {
        _liedjesManager.GekozenLiedje = lied;
        await LiedjeGekozen.InvokeAsync(); // Notify parent to close pop-up
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
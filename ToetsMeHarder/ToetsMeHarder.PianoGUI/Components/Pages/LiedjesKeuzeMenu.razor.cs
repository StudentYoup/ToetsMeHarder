using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business.LiedjesComponent;

namespace ToetsMeHarder.PianoGUI.Components.Pages;

public partial class LiedjesKeuzeMenu_razor : ComponentBase
{
    private LiedjesManager _liedjesManager = LiedjesManager.Instance;
    Liedje lied = new Liedje("TEST",120);
    
    [Parameter] public EventCallback OnOpen { get; set;}
    [Parameter] public EventCallback OnClose { get; set;}
    [Parameter] public bool IsOpen { get; set; } = false;

    public void SetLiedje()
    {
        _liedjesManager.GekozenLiedje = lied;
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
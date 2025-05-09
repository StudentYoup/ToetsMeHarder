using Microsoft.AspNetCore.Components;

namespace ToetsMeHarder.PianoGUI.Components.Pages;

public partial class MainMenu_razor : ComponentBase
{
    
    public bool IsMenuVisible = false;
    
    public bool LiedjeMenuOpen = false;

    public void Liedjemenu()
    {
        LiedjeMenuOpen = !LiedjeMenuOpen;
        IsMenuVisible = !IsMenuVisible;
    }

    public void Open()
    {
        IsMenuVisible = true;
        StateHasChanged();
    }

    public void Close() => IsMenuVisible = false;
}
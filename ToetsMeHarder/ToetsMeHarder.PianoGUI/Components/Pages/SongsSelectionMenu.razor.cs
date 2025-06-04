using Microsoft.AspNetCore.Components;
using ToetsMeHarder.Business.SongsComponent;

namespace ToetsMeHarder.PianoGUI.Components.Pages;

public partial class SongsSelectionMenu : ComponentBase
{
    private SongsManager _songManager = SongsManager.Instance;
    private List<Songs> _songList = new List<Songs>();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _songList = TestSongs.GetTestSongs();
    }
    [Parameter] public EventCallback OnOpen { get; set;}
    [Parameter] public EventCallback OnClose { get; set;}
    [Parameter] public bool IsOpen { get; set;} = false;
    [Parameter] public EventCallback SongChosen { get; set;}
    public async Task SetSong(Songs song)
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
        await Home.Instance.FocusWrapper();
        await OnClose.InvokeAsync();
        IsOpen = false;
    }
}
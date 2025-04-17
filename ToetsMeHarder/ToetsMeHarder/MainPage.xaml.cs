using ToetsMeHarder.Business;
using Plugin.Maui.Audio;

namespace ToetsMeHarder
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            AudioHandler audioHandler = new AudioHandler();
            audioHandler.PlayAudio(new Note(0,440,5000));
            InitializeComponent();
        }
    }
}

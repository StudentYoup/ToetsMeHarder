using System.Collections.Immutable;
using Plugin.Maui.Audio;

namespace ToetsMeHarder
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}

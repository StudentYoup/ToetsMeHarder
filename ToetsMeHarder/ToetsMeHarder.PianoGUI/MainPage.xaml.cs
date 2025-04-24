using System;
using Microsoft.Maui.Controls;
using ToetsMeHarder.Business;

namespace ToetsMeHarder.PianoGUI
{
  public partial class MainPage : ContentPage
    {  
        private readonly MetronomeService _metronome;

        public MainPage(MetronomeService metronome)
        {
            InitializeComponent();
    
        }
    }
}

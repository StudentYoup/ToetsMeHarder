using System;
using Microsoft.Maui.Controls;
using ToetsMeHarder.PianoGUI.Business;

namespace ToetsMeHarder.PianoGUI
{
  public partial class MainPage : ContentPage
{
    private readonly MetronomeService _metronome;

    public MainPage(MetronomeService metronome)
    {
        InitializeComponent();
        _metronome = metronome;

        BpmEntry.Text = _metronome.BPM.ToString();
        _metronome.Beat += (s, e) => 
		{ 
			//misschien uitbreiden met visuele flikkeren ofzoiets
		 };
    }



        void OnIncreaseClicked(object sender, EventArgs e)
        {
            _metronome.BPM++;
            BpmEntry.Text = _metronome.BPM.ToString();
        }

        void OnDecreaseClicked(object sender, EventArgs e)
        {
            _metronome.BPM--;
            BpmEntry.Text = _metronome.BPM.ToString();
        }

        void OnBpmEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(e.NewTextValue, out var bpm))
            {
                _metronome.BPM = bpm;
            }
        }

        void OnToggleClicked(object sender, EventArgs e)
        {
            if (_metronome.IsRunning)
            {
                _metronome.Stop();
                ToggleButton.Text = "Start";
            }
            else
            {
                _metronome.Start();
                ToggleButton.Text = "Stop";
            }
        }
    }
}

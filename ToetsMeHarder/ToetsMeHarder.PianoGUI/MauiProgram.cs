using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using ToetsMeHarder.Business;
using ToetsMeHarder.Business.Midi;
using ToetsMeHarder.PianoGUI.Components.Layout;
namespace ToetsMeHarder.PianoGUI;


public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddSingleton(AudioManager.Current);

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.AddAudio(); // voor geluiden
		builder.Services.AddSingleton<MetronomeService>(); // maar 1 instantie die door de applicatie gedeeld wordt
		builder.Services.AddTransient<MainPage>();

		//Midi
		builder.Services.AddSingleton<MidiService>();

		return builder.Build();
	}
}

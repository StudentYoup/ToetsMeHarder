using Microsoft.Extensions.DependencyInjection;

namespace ToetsMeHarder.PianoGUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
      	var mainPage = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<MainPage>(); // Haal de MainPage op uit de dependency injection container
        // Dit is de pagina die we willen tonen als de applicatie op start
        // Hier kunnen we ook een andere pagina instellen als dat nodig is
        // bvb: var mainPage = new anderepagine(); 

        // Maak een nieuwe Window aan met de MainPage
		return new Window(mainPage);

    }
}

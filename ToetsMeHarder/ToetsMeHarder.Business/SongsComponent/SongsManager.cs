using System.ComponentModel;

namespace ToetsMeHarder.Business.LiedjesComponent;

public class SongsManager
{
    //Dit is het singleton patroon dit zorgt ervoor dat er maar 1 van deze class aanwezig kan zijn.
    private static SongsManager _SongsManager;
    public static SongsManager Instance
    {
        get
        {
            if (_SongsManager == null)
            {
                _SongsManager = new SongsManager();
            }
            return _SongsManager;
        }
    }
    
    //dit blokeerd het maken van nieuwe instances van deze singleton
    private SongsManager(){}

    //het gekoze liedje kan overal veranderd worden. dit is makkelijk voor het gebruik in menu's.
    //de event kan gebruikt worden om ui te verversen bij het veranderen van deze propertie.
    //ook kan deze event in de toekomst gebruikt worden om andere properties door te sturen.
    private Songs _chosenSong = new Songs("Geen liedje Geselecteerd",60);
    public Songs ChosenSong
    {
        get => _chosenSong;
        set
        {
            _chosenSong = value;
            PropertyChangedEvent.Invoke(this, new PropertyChangedEventArgs(nameof(ChosenSong)));
        }
    }

    public List<Songs> MogelijkeLiedjes { get; private set; } = new List<Songs>();
    private event PropertyChangedEventHandler PropertyChangedEvent;
    public void RegisterPropertyChangedFunction(PropertyChangedEventHandler eventHandler)
    {
        PropertyChangedEvent += eventHandler;
    }
    
    
}
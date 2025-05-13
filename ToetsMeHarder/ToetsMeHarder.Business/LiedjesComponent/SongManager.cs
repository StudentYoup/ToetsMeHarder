using System.ComponentModel;

namespace ToetsMeHarder.Business.LiedjesComponent;

public class SongManager
{
    //Dit is het singleton patroon dit zorgt ervoor dat er maar 1 van deze class aanwezig kan zijn.
    private static SongManager _liedjesmanager;
    public static SongManager Instance
    {
        get
        {
            if (_liedjesmanager == null)
            {
                _liedjesmanager = new SongManager();
            }
            return _liedjesmanager;
        }
    }
    
    //dit blokeerd het maken van nieuwe instances van deze singleton
    private SongManager(){}

    //het gekoze liedje kan overal veranderd worden. dit is makkelijk voor het gebruik in menu's.
    //de event kan gebruikt worden om ui te verversen bij het veranderen van deze propertie.
    //ook kan deze event in de toekomst gebruikt worden om andere properties door te sturen.
    private Liedje _gekozenLiedje = new Liedje("Geen liedje Geselecteerd",60);
    public Liedje GekozenLiedje
    {
        get => _gekozenLiedje;
        set
        {
            _gekozenLiedje = value;
            PropertyChangedEvent.Invoke(this, new PropertyChangedEventArgs(nameof(GekozenLiedje)));
        }
    }

    public List<Liedje> MogelijkeLiedjes { get; private set; } = new List<Liedje>();
    private event PropertyChangedEventHandler PropertyChangedEvent;
    public void RegisterPropertyChangedFunction(PropertyChangedEventHandler eventHandler)
    {
        PropertyChangedEvent += eventHandler;
    }
    
    
}
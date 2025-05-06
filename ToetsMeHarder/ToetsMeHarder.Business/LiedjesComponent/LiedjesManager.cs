namespace ToetsMeHarder.Business.LiedjesComponent;

public class LiedjesManager
{
    private static LiedjesManager _liedjesmanager;
    public static LiedjesManager Instance
    {
        get
        {
            if (_liedjesmanager == null)
            {
                _liedjesmanager = new LiedjesManager();
            }

            return _liedjesmanager;
        }
    }

    public Liedje GekozenLiedje { get; set;}
    public List<Liedje> MogelijkeLiedjes { get; private set; } = new List<Liedje>();
    
    private LiedjesManager(){}
}
public class Parse
{
    private static Parse _instance;
    public static Parse instance
    {
        get
        {
            if (_instance == null)
                _instance = new Parse();

            return _instance;
        }
    }

    //--- подгрузка всех мечей
    private SwordCollection _swordClass;
    public SwordCollection swordClass
    {
        get
        {
            if (_swordClass == null)
                _swordClass = SwordCollection.Load();
    
            return _swordClass;
        }
    }
}

public class TouchEvents : GameEvent
{
    public static void CanvasTouch()
    {
        Events.instance.Raise(new EventCanvasTouch());
    }

    public static void CanvasUnTouch()
    {
        Events.instance.Raise(new EventCanvasUnTouch());
    }
}

//---касание канвы
public class EventCanvasTouch : TouchEvents
{
    public EventCanvasTouch()
    {
    }
}

//---касание вне канвы
public class EventCanvasUnTouch : TouchEvents
{
    public EventCanvasUnTouch()
    {
    }
}
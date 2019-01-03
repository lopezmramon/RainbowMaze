
public class LoadLevelRequestEvent : CodeControl.Message
{
    public RainbowColor color;
    public LoadLevelRequestEvent(RainbowColor color)
    {
        this.color = color;
    }
}

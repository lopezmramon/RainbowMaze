public class LevelIntroRequestEvent : CodeControl.Message
{
    public RainbowColor color;

    public LevelIntroRequestEvent(RainbowColor color)
    {
        this.color = color;
    }
}

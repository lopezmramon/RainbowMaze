
public class GenerateGridRequestEvent : CodeControl.Message
{
    public Level level;

    public GenerateGridRequestEvent
        (Level level)
    {
        this.level = level;
    }
}

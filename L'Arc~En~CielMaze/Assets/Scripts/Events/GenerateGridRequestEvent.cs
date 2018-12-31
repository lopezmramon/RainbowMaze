
public class GenerateGridRequestEvent : CodeControl.Message
{
    public int width, height, objectiveAmount, enemyAmount, exitAmount;
    public RainbowColors color;

    public GenerateGridRequestEvent
        (int width, int height, int objectiveAmount,
        int enemyAmount, int exitAmount, RainbowColors color)
    {
        this.width = width;
        this.height = height;
        this.objectiveAmount = objectiveAmount;
        this.enemyAmount = enemyAmount;
        this.exitAmount = exitAmount;
        this.color = color;
    }
}

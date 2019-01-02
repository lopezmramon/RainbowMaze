public class Level
{
    public int width, height, objectiveAmount, enemyAmount, exitAmount;
    public RainbowColor color;

    public Level(int width, int height, int objectiveAmount, int enemyAmount, int exitAmount, RainbowColor color)
    {
        this.width = width;
        this.height = height;
        this.objectiveAmount = objectiveAmount;
        this.enemyAmount = enemyAmount;
        this.exitAmount = exitAmount;
        this.color = color;
    }
}

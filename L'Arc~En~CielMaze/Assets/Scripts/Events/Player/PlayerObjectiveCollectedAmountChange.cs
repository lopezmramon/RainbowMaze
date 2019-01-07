public class PlayerObjectiveCollectedAmountChange : CodeControl.Message
{
    public int totalObjectivesToCollect, objectivesCollected;

    public PlayerObjectiveCollectedAmountChange(int totalObjectivesToCollect, int objectivesCollected)
    {
        this.totalObjectivesToCollect = totalObjectivesToCollect;
        this.objectivesCollected = objectivesCollected;
    }
}

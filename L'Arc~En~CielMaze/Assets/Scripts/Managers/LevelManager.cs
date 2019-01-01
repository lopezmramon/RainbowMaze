using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int objectivesCollected;
    public float timePlayingLevel;
    private int stepsTaken;
    public ScriptableObject levels;
    private Level currentLevel;
    private bool countingTime;

    private void Awake()
    {
        CodeControl.Message.AddListener<ObjectiveCollectedEvent>(OnObjectiveCollected);
        CodeControl.Message.AddListener<PlayerMoveResolvedEvent>(OnPlayerMoveResolved);
    }

    private void Start()
    {
        currentLevel = new Level(10, 10, 5, 5, 1, RainbowColors.Violet);
        DispatchGridRequestEvent();
    }

    private void Update()
    {
        if (countingTime)
        {
            timePlayingLevel += Time.deltaTime;
        }
    }

    private void OnPlayerMoveResolved(PlayerMoveResolvedEvent obj)
    {
        if (obj.approved)
        {
            stepsTaken++;
            if (!countingTime) countingTime = true;
        }

    }

    private void OnObjectiveCollected(ObjectiveCollectedEvent obj)
    {
        objectivesCollected++;
        Destroy(obj.transform.gameObject);
        if (currentLevel.objectiveAmount <= objectivesCollected)
        {
            DispatchLevelCompleteEvent();
            countingTime = false;
        }
    }

    private void DispatchLevelCompleteEvent()
    {
        Debug.Log(string.Format("Level Completed. Time: {0}, Steps: {1}", timePlayingLevel, stepsTaken));
        CodeControl.Message.Send(new LevelCompleteEvent(timePlayingLevel, stepsTaken));
    }

    private void DispatchGridRequestEvent()
    {
        CodeControl.Message.Send(new GenerateGridRequestEvent(currentLevel));
    }
}

using System;
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
        CodeControl.Message.AddListener<LoadLevelRequestEvent>(OnLevelLoadRequested);
        CodeControl.Message.AddListener<ObjectiveCollectedEvent>(OnObjectiveCollected);
        CodeControl.Message.AddListener<PlayerMoveResolvedEvent>(OnPlayerMoveResolved);
        CodeControl.Message.AddListener<ExitContactEvent>(OnExitContact);
    }

    private void Start()
    {
      
        DispatchPlayMusicRequestEvent();
    }

    private void Update()
    {
        if (countingTime)
        {
            timePlayingLevel += Time.deltaTime;
        }
    }

    private void OnLevelLoadRequested(LoadLevelRequestEvent obj)
    {
        currentLevel = new Level(10, 10, 5, 5, 1, RainbowColor.Violet);
        DispatchGridRequestEvent();
    }

    private void OnPlayerMoveResolved(PlayerMoveResolvedEvent obj)
    {
        if (obj.approved)
        {
            stepsTaken++;
            if (!countingTime) countingTime = true;
        }

    }

    private void OnExitContact(ExitContactEvent obj)
    {
        if (currentLevel.objectiveAmount > objectivesCollected) return;
        DispatchLevelCompleteEvent();
        countingTime = false;
    }

    private void OnObjectiveCollected(ObjectiveCollectedEvent obj)
    {
        objectivesCollected++;
        Destroy(obj.transform.gameObject);
        if (currentLevel.objectiveAmount <= objectivesCollected)
        {
            DispatchAllObjectivesCollectedEvent();
            DispatchPlaySFXRequestEvent("objectivescollected", 0.7f);
        }
        else
        {
            DispatchPlaySFXRequestEvent("objective" + objectivesCollected.ToString(), 0.4f);
        }
    }

    private void DispatchAllObjectivesCollectedEvent()
    {
        CodeControl.Message.Send(new AllObjectivesCollectedEvent());
    }

    private void DispatchLevelCompleteEvent()
    {
        Debug.Log(string.Format("Level Completed. Time: {0}, Steps: {1}", timePlayingLevel, stepsTaken));
        CodeControl.Message.Send(new LevelCompleteEvent(timePlayingLevel, stepsTaken, currentLevel.color));
        DispatchPlaySFXRequestEvent("levelwin", 0.5f);
    }

    private void DispatchGridRequestEvent()
    {
        CodeControl.Message.Send(new GenerateGridRequestEvent(currentLevel));
    }

    private void DispatchPlaySFXRequestEvent(string sfxName, float volume)
    {
        CodeControl.Message.Send(new PlaySFXRequestEvent(sfxName, volume));
    }

    private void DispatchPlayMusicRequestEvent()
    {
        CodeControl.Message.Send(new PlayMusicRequestEvent("enterthemaze"));
    }

}

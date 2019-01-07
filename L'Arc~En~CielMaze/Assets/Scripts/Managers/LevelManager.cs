using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int objectivesCollected;
    public float timePlayingLevel;
    private int stepsTaken;
    private Level currentLevel;
    private Level[] levels;
    private bool countingTime;
    public Material[] skyboxes;

    private void Awake()
    {
        CodeControl.Message.AddListener<LoadLevelRequestEvent>(OnLevelLoadRequested);
        CodeControl.Message.AddListener<ObjectiveCollectedEvent>(OnObjectiveCollected);
        CodeControl.Message.AddListener<PlayerMoveResolvedEvent>(OnPlayerMoveResolved);
        CodeControl.Message.AddListener<ExitContactEvent>(OnExitContact);
        levels = new Level[]
        {
            new Level(8, 8, 4, 3, 1, RainbowColor.Violet),
            new Level(9, 9, 5, 4, 1, RainbowColor.Indigo),
            new Level(10, 10,6, 5, 1, RainbowColor.Blue),
            new Level(10, 10, 7, 6, 1, RainbowColor.Green),
            new Level(10, 10, 7, 6, 1, RainbowColor.Yellow),
            new Level(11, 11, 7, 7, 2, RainbowColor.Orange),
            new Level(12, 12, 8, 9, 2, RainbowColor.Red),
        };
    }

    private void Start()
    {
        DispatchPlayMusicRequestEvent();
        SwitchToRandomSkybox();
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
        currentLevel = levels[(int)obj.color];
        objectivesCollected = 0;
        timePlayingLevel = 0;
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
        if (currentLevel.objectiveAmount > objectivesCollected)
        {
            DispatchPlaySFXRequestEvent("hurt" + UnityEngine.Random.Range(1, 3).ToString(), 0.5f);
            return;
        }
        DispatchLevelCompleteEvent();
        countingTime = false;
        Destroy(obj.transform.gameObject);
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
        DispatchPlayerObjectiveCollectedAmountChange();
    }

    private void SwitchToRandomSkybox()
    {
        Camera.main.GetComponent<Skybox>().material = skyboxes[UnityEngine.Random.Range(0, skyboxes.Length)];
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

    private void DispatchPlayerObjectiveCollectedAmountChange()
    {
        CodeControl.Message.Send(new PlayerObjectiveCollectedAmountChange(currentLevel.objectiveAmount, objectivesCollected));
    }
}

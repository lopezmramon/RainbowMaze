using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LevelFinishedViewController : MonoBehaviour
{
    public Button nextLevelButton, mainMenuButton, quitButton;
    public Text timeForCompletionText, stepsTakenText;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            UIMessageDispatcher.DispatchViewChangeRequestEvent(View.MainMenu);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    internal void Initialize(float timeForCompletion, int stepsTaken, RainbowColor color)
    {
        ConfigureNextLevelButton(color);
        timeForCompletionText.text = string.Format("Level Completed in {0} seconds!", timeForCompletion);
        stepsTakenText.text = string.Format("Steps taken: {0}", stepsTaken);

    }

    private void ConfigureNextLevelButton(RainbowColor color)
    {
        nextLevelButton.onClick.RemoveAllListeners();

    }
}

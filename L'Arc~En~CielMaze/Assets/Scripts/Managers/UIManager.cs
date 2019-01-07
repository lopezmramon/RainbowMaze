using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] views;

    private void Awake()
    {
        CodeControl.Message.AddListener<ViewChangeRequestEvent>(OnViewChangeRequested);
        CodeControl.Message.AddListener<LevelCompleteEvent>(OnLevelCompleted);
    }

    private void OnLevelCompleted(LevelCompleteEvent obj)
    {
        ChangeView(View.LevelFinished);
        views[(int)View.LevelFinished].GetComponent<LevelFinishedViewController>().Initialize(obj.timeForCompletion, obj.stepsTaken, obj.color);
    }

    private void Start()
    {
        ChangeView(View.MainMenu);
    }

    private void OnViewChangeRequested(ViewChangeRequestEvent obj)
    {
        ChangeView(obj.view);
        if (obj.view == View.LevelIntro)
        {
            views[(int)obj.view].GetComponent<LevelIntroViewController>().Initialize((RainbowColor)obj.usableValue);
        }
    }

    private void ChangeView(View view)
    {
        DeactivateAllViews();
        views[(int)view].SetActive(true);
    }

    private void DeactivateAllViews()
    {
        foreach (GameObject view in views)
        {
            view.SetActive(false);
        }
    }
}

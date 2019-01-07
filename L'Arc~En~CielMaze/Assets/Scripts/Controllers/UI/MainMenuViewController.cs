using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuViewController : MonoBehaviour
{
    public Button startGameButton, quitGameButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(() =>
        {
            UIMessageDispatcher.DispatchViewChangeRequestEvent(View.LevelIntro, 0);
        });

        quitGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}

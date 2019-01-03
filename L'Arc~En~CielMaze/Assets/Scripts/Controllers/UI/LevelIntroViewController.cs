using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntroViewController : MonoBehaviour
{
    private RainbowColor color;
    private float animationTimer;

    public void Initialize(RainbowColor color)
    {
        this.color = color;
        animationTimer = 5;
    }

    private void Update()
    {
        animationTimer -= Time.deltaTime;
        if (animationTimer < 0)
        {
            DispatchLoadLevelRequestEvent();
            UIMessageDispatcher.DispatchViewChangeRequestEvent(View.Gameplay);
            animationTimer += 10;
        }
    }

    private void DispatchLoadLevelRequestEvent()
    {
        CodeControl.Message.Send(new LoadLevelRequestEvent(RainbowColor.Violet));
    }
}

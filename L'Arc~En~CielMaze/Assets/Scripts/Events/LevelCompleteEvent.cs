using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteEvent : CodeControl.Message
{
    public float timeForCompletion;
    public int stepsTaken;
    public RainbowColor color;
    public LevelCompleteEvent(float timeForCompletion, int stepsTaken, RainbowColor color)
    {
        this.timeForCompletion = timeForCompletion;
        this.stepsTaken = stepsTaken;
        this.color = color;
    }
}

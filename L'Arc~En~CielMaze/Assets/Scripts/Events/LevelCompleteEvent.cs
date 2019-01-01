using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteEvent : CodeControl.Message
{
    public float timeForCompletion;
    public int stepsTaken;

    public LevelCompleteEvent(float timeForCompletion, int stepsTaken)
    {
        this.timeForCompletion = timeForCompletion;
        this.stepsTaken = stepsTaken;
    }
}

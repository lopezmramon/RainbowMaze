using UnityEngine;

public class ObjectiveCollectedEvent : CodeControl.Message
{
    public Transform transform;
    public RainbowColor color;

    public ObjectiveCollectedEvent(Transform transform, RainbowColor color)
    {
        this.transform = transform;
        this.color = color;
    }
}

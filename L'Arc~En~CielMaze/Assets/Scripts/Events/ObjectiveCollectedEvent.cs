using UnityEngine;

public class ObjectiveCollectedEvent : CodeControl.Message
{
    public Transform transform;

    public ObjectiveCollectedEvent(Transform transform)
    {
        this.transform = transform;
    }
}

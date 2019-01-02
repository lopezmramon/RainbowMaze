using UnityEngine;

public class ExitContactEvent : CodeControl.Message
{
    public Transform transform;

    public ExitContactEvent(Transform transform)
    {
        this.transform = transform;
    }
}

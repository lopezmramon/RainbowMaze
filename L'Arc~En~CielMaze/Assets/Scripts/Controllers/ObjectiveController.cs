using UnityEngine;

public class ObjectiveController : BasePickup
{
    public RainbowColor color;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DispatchObjectiveCollectedEvent();
        }
    }

    private void DispatchObjectiveCollectedEvent()
    {
        CodeControl.Message.Send(new ObjectiveCollectedEvent(transform, color));
    }
}

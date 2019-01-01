using UnityEngine;

public class ObjectiveController : BasePickup
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DispatchObjectiveCollectedEvent();
        }
    }

    private void DispatchObjectiveCollectedEvent()
    {
        CodeControl.Message.Send(new ObjectiveCollectedEvent(transform));
    }
}

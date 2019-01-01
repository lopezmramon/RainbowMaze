using UnityEngine;

public class PickupController : BasePickup
{
    public Pickup pickup;

    public void Initialize(Pickup pickup)
    {
        this.pickup = pickup;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DispatchEnemyContactEvent();
        }
    }

    private void DispatchEnemyContactEvent()
    {
        CodeControl.Message.Send(new PickupContactEvent(pickup, transform));
    }
}

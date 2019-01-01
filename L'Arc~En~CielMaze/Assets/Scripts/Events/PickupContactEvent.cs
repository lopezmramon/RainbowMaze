using UnityEngine;

public class PickupContactEvent : CodeControl.Message
{
    public Pickup pickup;
    public Transform transform;
    public PickupContactEvent(Pickup pickup, Transform transform)
    {
        this.pickup = pickup;
        this.transform = transform;
    }
}

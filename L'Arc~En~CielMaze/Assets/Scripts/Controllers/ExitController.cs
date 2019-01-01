using UnityEngine;

public class ExitController : BasePickup
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DispatchExitContactEvent();
        }
    }

    private void DispatchExitContactEvent()
    {
        CodeControl.Message.Send(new ExitContactEvent(transform));
    }
}

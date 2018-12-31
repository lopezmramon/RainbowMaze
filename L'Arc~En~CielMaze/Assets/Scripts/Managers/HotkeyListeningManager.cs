using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyListeningManager : MonoBehaviour
{
    private InputMessageDispatcher inputMessenger;

    private void Awake()
    {
        inputMessenger = new InputMessageDispatcher();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Directions.North);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Directions.South);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Directions.East);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Directions.West);
        }
    }

}

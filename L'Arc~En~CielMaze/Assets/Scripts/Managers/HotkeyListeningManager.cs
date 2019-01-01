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
        if (Input.GetKeyDown(KeyCode.W))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Direction.South);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Direction.North);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Direction.East);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Direction.West);
        }
    }

}

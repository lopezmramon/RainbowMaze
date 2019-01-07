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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Direction.South);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Direction.North);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Direction.East);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            inputMessenger.DispatchPlayerMoveRequestEvent(Direction.West);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // inputMessenger.DispatchPlayerPointOfViewChangeRequestEvent();
        }
    }
}

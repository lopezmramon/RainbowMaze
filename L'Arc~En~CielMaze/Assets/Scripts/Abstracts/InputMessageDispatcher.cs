using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMessageDispatcher
{   
    public void DispatchPlayerMoveRequestEvent(Directions direction)
    {
        CodeControl.Message.Send(new PlayerMoveRequestEvent(direction));
    }
}

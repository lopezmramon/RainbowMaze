using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRequestEvent : CodeControl.Message
{
    public Directions direction;

    public PlayerMoveRequestEvent(Directions direction)
    {
        this.direction = direction;
    }
}

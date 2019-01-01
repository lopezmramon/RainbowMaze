using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveRequestEvent : CodeControl.Message
{
    public Direction direction;

    public PlayerMoveRequestEvent(Direction direction)
    {
        this.direction = direction;
    }
}

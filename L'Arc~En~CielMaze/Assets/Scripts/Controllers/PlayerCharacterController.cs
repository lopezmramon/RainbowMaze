using System;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private void Awake()
    {
        CodeControl.Message.AddListener<PlayerMoveResolvedEvent>(OnPlayerMoveResolved);
    }

    private void OnPlayerMoveResolved(PlayerMoveResolvedEvent obj)
    {
        if(obj.approved)
        MoveToDirection(obj.direction);
    }

    private void MoveToDirection(Direction direction)
    {
        Vector3 finalPosition = transform.localPosition;
        finalPosition.x += DirectionRelations.DirectionX[direction] * 10;
        finalPosition.z += DirectionRelations.DirectionY[direction] * 10;
        transform.localPosition = finalPosition;
    }
}

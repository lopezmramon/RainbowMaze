using System;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private new Light light;

    private void Awake()
    {
        CodeControl.Message.AddListener<PlayerMoveResolvedEvent>(OnPlayerMoveResolved);
        light = GetComponent<Light>();
    }

    private void OnPlayerMoveResolved(PlayerMoveResolvedEvent obj)
    {
        if (obj.approved)
            MoveToDirection(obj.direction);
    }

    private void MoveToDirection(Direction direction)
    {
        Vector3 finalPosition = transform.localPosition;
        finalPosition.x += DirectionRelations.DirectionX[direction] * 10;
        finalPosition.z += DirectionRelations.DirectionY[direction] * 10;
        transform.localPosition = finalPosition;
    }

    public void ChangeLightRange(int range)
    {
        light.range = range;
        light.intensity = (float)range / 6;
    }

    public void ChangeLightColor(Color color)
    {
        light.color = color;
    }
}

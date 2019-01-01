using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Cell[,] cells;
    private Cell currentPlayerCell;

    private void Awake()
    {
        CodeControl.Message.AddListener<PlayerMoveRequestEvent>(OnPlayerMoveRequested);
        CodeControl.Message.AddListener<GridGeneratedEvent>(OnGridGenerated);
    }

    private void OnGridGenerated(GridGeneratedEvent obj)
    {
        cells = obj.cells;
        currentPlayerCell = obj.playerCell;
    }

    private void OnPlayerMoveRequested(PlayerMoveRequestEvent obj)
    {
        if (!ExistingCell(currentPlayerCell.coordinates.x + DirectionRelations.DirectionX[obj.direction],
            currentPlayerCell.coordinates.y + DirectionRelations.DirectionY[obj.direction])) return;
        Cell desiredDestinationCell =
            cells[currentPlayerCell.coordinates.x + DirectionRelations.DirectionX[obj.direction],
            currentPlayerCell.coordinates.y + DirectionRelations.DirectionY[obj.direction]];
        DispatchPlayerMoveEvent(obj.direction, !currentPlayerCell.walls[obj.direction]);
        if (!currentPlayerCell.walls[obj.direction])
        {
            currentPlayerCell = desiredDestinationCell;
        }
    }

    private void DispatchPlayerMoveEvent(Direction direction, bool approved)
    {
        CodeControl.Message.Send(new PlayerMoveResolvedEvent(direction, approved));
    }

    public bool ExistingCell(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < cells.GetLength(0) && y < cells.GetLength(1));
    }
}

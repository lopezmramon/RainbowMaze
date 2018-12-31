using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Cell[,] cells;

    private void Awake()
    {
        CodeControl.Message.AddListener<PlayerMoveRequestEvent>(OnPlayerMoveRequested);
        CodeControl.Message.AddListener<GridGeneratedEvent>(OnGridGenerated);
    }

    private void Start()
    {
        DispatchGridRequestEvent();
    }

    private void OnGridGenerated(GridGeneratedEvent obj)
    {
        cells = obj.cells;
    }

    private void OnPlayerMoveRequested(PlayerMoveRequestEvent obj)
    {
        throw new NotImplementedException();
    }

    private void DispatchGridRequestEvent()
    {
        CodeControl.Message.Send(new GenerateGridRequestEvent(10, 10, 5, 5, 1, RainbowColors.Violet));
    }
}

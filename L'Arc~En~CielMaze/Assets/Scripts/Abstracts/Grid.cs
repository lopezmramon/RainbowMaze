using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Grid
{
    private const int _rowDimension = 0;
    private const int _columnDimension = 1;

    public Cell[,] cells { get; private set; }

    public void SetupGrid(int rows, int columns)
    {
        if (cells != null) cells = null;
        cells = new Cell[rows, columns];
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                cells[i, j] = new Cell(i, j);
            }
        }
    }

    public Cell[,] Generate(int width, int height)
    {
        SetupGrid(width, height);
        CarvePassagesFrom(0, 0, cells);
        return cells;
    }

    private void CarvePassagesFrom(int startingX, int startingY, Cell[,] cells)
    {
        List<Directions> directionsForGrid = new List<Directions>
            { Directions.North, Directions.South, Directions.East, Directions.West };
        directionsForGrid.Shuffle();

        int currentX = startingX;
        int currentY = startingY;
        for (int i = 0; i < 4; i++)
        {
            int nextX = currentX + DirectionRelations.DirectionX[directionsForGrid[i]];
            int nextY = currentY + DirectionRelations.DirectionY[directionsForGrid[i]];
            if (ValidCell(nextX, nextY))
            {
                Directions oppositeDirection = DirectionRelations.Opposite[directionsForGrid[i]];
                cells[currentX, currentY].walls[directionsForGrid[i]] = false;
                cells[nextX, nextY].walls[oppositeDirection] = false;
                cells[nextX, nextY].visited = true;
                CarvePassagesFrom(nextX, nextY, cells);
            }
        }
    }

    private bool ValidCell(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < cells.GetLength(0) && y < cells.GetLength(1) && !cells[x, y].visited);
    }
}

using System.Collections.Generic;
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
        List<Direction> directionsForGrid = new List<Direction>
            { Direction.North, Direction.South, Direction.East, Direction.West };
        directionsForGrid.Shuffle();

        int currentX = startingX;
        int currentY = startingY;
        for (int i = 0; i < 4; i++)
        {
            int nextX = currentX + DirectionRelations.DirectionX[directionsForGrid[i]];
            int nextY = currentY + DirectionRelations.DirectionY[directionsForGrid[i]];
            if (ValidCell(nextX, nextY))
            {
                Direction oppositeDirection = DirectionRelations.Opposite[directionsForGrid[i]];
                cells[currentX, currentY].walls[directionsForGrid[i]] = false;
                cells[nextX, nextY].walls[oppositeDirection] = false;
                cells[nextX, nextY].visited = true;
                CarvePassagesFrom(nextX, nextY, cells);
            }
        }
    }

    public bool ValidCell(int x, int y)
    {
        return (x >= 0 && y >= 0 && x < cells.GetLength(0) && y < cells.GetLength(1) && !cells[x, y].visited);
    }

    public int DistanceToCell(Cell origin, Cell destination)
    {
        if (origin == null) return 10000;
        return Mathf.Abs(origin.coordinates.x - destination.coordinates.x) +
            Mathf.Abs(origin.coordinates.y - destination.coordinates.y);
    }

    public Cell RandomCell(Cell playerCell, int minDistanceToPlayer)
    {
        int randomCoordinateX = Random.Range(0, cells.GetLength(0));
        int randomCoordinateY = Random.Range(0, cells.GetLength(1));
        if (minDistanceToPlayer > cells.GetLength(1) / 2) minDistanceToPlayer = cells.GetLength(1) / 2;
        Cell tentativeCell = cells[randomCoordinateX, randomCoordinateY];
        while (tentativeCell.occupied ||
            tentativeCell.SurroundingWallAmount() == 4 ||
            DistanceToCell(playerCell, tentativeCell) < minDistanceToPlayer)
        {
            randomCoordinateX = Random.Range(0, cells.GetLength(0));
            randomCoordinateY = Random.Range(0, cells.GetLength(1));
            tentativeCell = cells[randomCoordinateX, randomCoordinateY];
        }
        return tentativeCell;
    }
}

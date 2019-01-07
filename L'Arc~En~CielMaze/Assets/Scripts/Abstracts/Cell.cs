using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2Int coordinates;
    public Dictionary<Direction, bool> walls;
    public bool visited;
    public bool occupied;

    public Cell(int x, int y)
    {
        coordinates = new Vector2Int(x, y);
        walls = new Dictionary<Direction, bool>();
        walls.Add(Direction.North, true);
        walls.Add(Direction.South, true);
        walls.Add(Direction.East, true);
        walls.Add(Direction.West, true);
    }

    public int SurroundingWallAmount()
    {
        int activeWalls = 0;
        foreach (KeyValuePair<Direction, bool> wall in walls)
        {
            if (wall.Value) activeWalls++;
        }
        return activeWalls;
    }

    public Vector3 SpawnOverCellLocalPosition(float xOffset, float yOffset, float zOffset)
    {
        return new Vector3(coordinates.x * 10 + xOffset, yOffset, coordinates.y * 10 + zOffset);
    }
}

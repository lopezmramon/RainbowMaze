using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2Int coordinates;
    public Dictionary<Directions, bool> walls;
    public bool visited;
    public bool occupied;

    public Cell(int x, int y)
    {
        coordinates = new Vector2Int(x, y);
        walls = new Dictionary<Directions, bool>();
        walls.Add(Directions.North, true);
        walls.Add(Directions.South, true);
        walls.Add(Directions.East, true);
        walls.Add(Directions.West, true);
    }
}

using System.Collections.Generic;

public enum Direction
{
    North = 1,
    South = 2,
    East = 4,
    West = 8
}

public static class DirectionRelations
{

    public static Dictionary<Direction, int> DirectionX = new Dictionary<Direction, int>
        {
            { Direction.North, 0 },
            { Direction.South, 0 },
            { Direction.East, 1 },
            { Direction.West, -1 }
        };

    public static Dictionary<Direction, int> DirectionY = new Dictionary<Direction, int>
        {
            { Direction.North, -1 },
            { Direction.South, 1 },
            { Direction.East, 0 },
            { Direction.West, 0 }
        };

    public static Dictionary<Direction, Direction> Opposite = new Dictionary<Direction, Direction>
        {
            { Direction.North, Direction.South },
            { Direction.South, Direction.North },
            { Direction.East, Direction.West },
            { Direction.West, Direction.East }
        };
}
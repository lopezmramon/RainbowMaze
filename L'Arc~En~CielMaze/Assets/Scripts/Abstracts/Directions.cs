using System.Collections.Generic;

public enum Directions
{
    North = 1,
    South = 2,
    East = 4,
    West = 8
}

public static class DirectionRelations
{

    public static Dictionary<Directions, int> DirectionX = new Dictionary<Directions, int>
        {
            { Directions.North, 0 },
            { Directions.South, 0 },
            { Directions.East, 1 },
            { Directions.West, -1 }
        };

    public static Dictionary<Directions, int> DirectionY = new Dictionary<Directions, int>
        {
            { Directions.North, -1 },
            { Directions.South, 1 },
            { Directions.East, 0 },
            { Directions.West, 0 }
        };

    public static Dictionary<Directions, Directions> Opposite = new Dictionary<Directions, Directions>
        {
            { Directions.North, Directions.South },
            { Directions.South, Directions.North },
            { Directions.East, Directions.West },
            { Directions.West, Directions.East }
        };
}
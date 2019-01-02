
using UnityEngine;

public enum RainbowColor
{
    Violet,
    Indigo,
    Blue,
    Green,
    Yellow,
    Orange,
    Red
}

public static class RainbowColorReference
{
    public static Color UsableRainbowColor(RainbowColor color)
    {
        switch (color)
        {
            case RainbowColor.Blue:
                return Color.blue;
            case RainbowColor.Green:
                return Color.green;
            case RainbowColor.Yellow:
                return Color.yellow;
            case RainbowColor.Red:
                return Color.red;
            case RainbowColor.Orange:
                return new Color(255, 165, 0);
            case RainbowColor.Violet:
                return new Color(238, 112, 214);
            case RainbowColor.Indigo:
                return new Color(75, 0, 130);
        }
        return Color.white;
    }
}


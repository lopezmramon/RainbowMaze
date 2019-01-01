using UnityEngine;

public class GeneratePlayerCharacterRequestEvent : CodeControl.Message
{
    public Cell cell;
    public Transform cellParent;
    public GeneratePlayerCharacterRequestEvent(Cell cell, Transform cellParent)
    {
        this.cell = cell;
        this.cellParent = cellParent;
    }
}

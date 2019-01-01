public class GridGeneratedEvent : CodeControl.Message
{
    public Cell[,] cells;
    public Cell playerCell;

    public GridGeneratedEvent(Cell[,] cells, Cell playerCell)
    {
        this.cells = cells;
        this.playerCell = playerCell;
    }
}

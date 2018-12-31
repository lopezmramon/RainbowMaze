public class GridGeneratedEvent : CodeControl.Message
{
    public Cell[,] cells;

    public GridGeneratedEvent(Cell[,] cells)
    {
        this.cells = cells;
    }
}

public class PlayerMoveResolvedEvent : CodeControl.Message
{
    public Direction direction;
    public bool approved;

    public PlayerMoveResolvedEvent(Direction direction, bool approved)
    {
        this.direction = direction;
        this.approved = approved;
    }
}

public class InputMessageDispatcher
{   
    public void DispatchPlayerMoveRequestEvent(Direction direction)
    {
        CodeControl.Message.Send(new PlayerMoveRequestEvent(direction));
    }
}

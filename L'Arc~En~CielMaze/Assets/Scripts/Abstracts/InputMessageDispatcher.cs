using System;

public class InputMessageDispatcher
{
    public void DispatchPlayerMoveRequestEvent(Direction direction)
    {
        CodeControl.Message.Send(new PlayerMoveRequestEvent(direction));
    }

    internal void DispatchPlayerPointOfViewChangeRequestEvent()
    {
        CodeControl.Message.Send(new PlayerPointOfViewChangeRequestEvent());
    }
}


public static class UIMessageDispatcher
{
    public static void DispatchViewChangeRequestEvent(View view, int usableValue)
    {
        CodeControl.Message.Send(new ViewChangeRequestEvent(view, usableValue));
    }
}

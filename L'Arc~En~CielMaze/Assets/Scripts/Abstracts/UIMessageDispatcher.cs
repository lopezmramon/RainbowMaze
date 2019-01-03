
public static class UIMessageDispatcher
{
    public static void DispatchViewChangeRequestEvent(View view)
    {
        CodeControl.Message.Send(new ViewChangeRequestEvent(view));
    }
}

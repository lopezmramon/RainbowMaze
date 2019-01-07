
public class ViewChangeRequestEvent : CodeControl.Message
{
    public View view;
    public int usableValue;

    public ViewChangeRequestEvent(View view, int usableValue)
    {
        this.view = view;
        this.usableValue = usableValue;
    }
}

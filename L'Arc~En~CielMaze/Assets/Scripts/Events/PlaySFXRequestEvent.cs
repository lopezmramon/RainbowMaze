public class PlaySFXRequestEvent : CodeControl.Message
{
    public string sfxName;
    public float volume;

    public PlaySFXRequestEvent(string sfxName, float volume)
    {
        this.sfxName = sfxName;
        this.volume = volume;
    }
}

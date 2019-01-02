public class PlayMusicRequestEvent : CodeControl.Message
{
    public string songName;

    public PlayMusicRequestEvent(string songName)
    {
        this.songName = songName;
    }
}

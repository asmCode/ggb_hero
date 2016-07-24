class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    public bool MusicEnabled { get; private set; }
    public bool SoundsEnabled { get; private set; }

    public void SetMusicEnabled(bool enabled)
    {
        MusicEnabled = enabled;
    }

    public void SetSoundsEnabled(bool enabled)
    {
        SoundsEnabled = enabled;
    }
}

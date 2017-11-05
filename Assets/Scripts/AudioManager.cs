using UnityEngine;

class AudioManager : MonoBehaviourSingleton<AudioManager, AudioManager.Meta>
{
    public class Meta : MonoBehaviourSingletonMeta
    {
        public override string PrefabName
        {
            get
            {
                return "AudioManager";
            }
        }
    }

    private bool m_isInitialized = false;

    public Sound SoundAmbient;
    public Sound SoundCatch;
    public Sound SoundDie;
    public Sound SoundEndRound;
    public Sound SoundJump;
    public Sound SoundJumpBridge;
    public Sound SoundLand;
    public Sound SoundSave;
    public Sound SoundSummary;
    public Sound SoundSwim;
    public Sound SoundWaterSplash;  

    public bool MusicEnabled { get; private set; }
    public bool SoundsEnabled { get; private set; }

    public void SetMusicEnabled(bool enabled)
    {
        MusicEnabled = enabled;
    }

    public void SetSoundsEnabled(bool enabled)
    {
        SoundsEnabled = enabled;
        PlayerPrefs.SetInt("audiomanager.sound_enabled", enabled ? 1 : 0);

        if (enabled)
            SoundAmbient.Play();
        else
            SoundAmbient.Stop();

        AudioListener.volume = enabled ? 1.0f : 0.0f;
    }

    protected override void Awake()
    {
        if (!m_isInitialized)
        {
            Init();
        }
        //base.Awake();

        m_instance = this;

        SoundAmbient = transform.FindChild("Ambient").GetComponent<Sound>();
        SoundCatch = transform.FindChild("Catch").GetComponent<Sound>();
        SoundDie = transform.FindChild("Die").GetComponent<Sound>();
        SoundEndRound = transform.FindChild("EndRound").GetComponent<Sound>();
        SoundJump = transform.FindChild("Jump").GetComponent<Sound>();
        SoundJumpBridge = transform.FindChild("JumpBridge").GetComponent<Sound>();
        SoundLand = transform.FindChild("Land").GetComponent<Sound>();
        SoundSave = transform.FindChild("Save").GetComponent<Sound>();
        SoundSummary = transform.FindChild("Summary").GetComponent<Sound>();
        SoundSwim = transform.FindChild("Swim").GetComponent<Sound>();
        SoundWaterSplash = transform.FindChild("WaterSplash").GetComponent<Sound>();
    }

    private void Init()
    {
        SoundsEnabled = PlayerPrefs.GetInt("audiomanager.sound_enabled", 1) == 1;
        m_isInitialized = true;
    }
}

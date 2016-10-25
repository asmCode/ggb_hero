using UnityEngine;

class Sound : MonoBehaviour
{
    private AudioSource m_audioSource;

    public void Play()
    {
        if (AudioManager.GetInstance().SoundsEnabled)
            m_audioSource.Play();
    }

    public void Stop()
    {
        m_audioSource.Stop();
    }

    public bool IsPlaying()
    {
        return m_audioSource.isPlaying;
    }

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
}

//using UnityEngine;

//class Sound
//{
//    private AudioSource m_audioSource;

//    public void Init()
//    {
//        var gameObject = new GameObject();
//        m_audioSource = gameObject.AddComponent<AudioSource>();
//        m_audioSource.clip = Resources.Load<A>
//    }

//    public void Play()
//    {

//    }

//    public Sound Button { get; private set; }

//    public bool MusicEnabled { get; private set; }
//    public bool SoundsEnabled { get; private set; }

//    public void SetMusicEnabled(bool enabled)
//    {
//        MusicEnabled = enabled;
//    }

//    public void SetSoundsEnabled(bool enabled)
//    {
//        SoundsEnabled = enabled;
//    }

//    protected override void Awake()
//    {
//        base.Awake();


//    }
//}

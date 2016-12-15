using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    public Transform m_story = null;
    public GameObject m_topfade = null;
    public Texture2D m_bgNormal;
    public Texture2D m_bgIos;
    public UITexture m_bgUiTexture;

    private Animator m_storyAnimator;
    private float[] m_stopTimes;
    private float m_animationLength = 0;

    private bool m_storyStarted;

    private void Start()
    {
        SetBgTexture();

        PlayerPrefs.SetInt("skip_intro", 1);

        if (!AudioManager.GetInstance().SoundAmbient.IsPlaying())
            AudioManager.GetInstance().SoundAmbient.Play();

        m_storyAnimator = m_story.GetComponent<Animator>();
        int storyClipIndex = GetClipIndex("Story");
        m_animationLength = m_storyAnimator.runtimeAnimatorController.animationClips[storyClipIndex].length;

        AnimationMarkers animationMarkers = m_story.GetComponent<AnimationMarkers>();
        m_stopTimes = new float[animationMarkers.Markers.Length];
        System.Array.Copy(animationMarkers.Markers, m_stopTimes, animationMarkers.Markers.Length);

        // Delay is required cos there are lags on the ios and the game starts with the animation started.
        Invoke("StartStory", 1.0f);
    }

    private void StartStory()
    {
        m_topfade.gameObject.SetActive(false);
        m_storyAnimator.Play("Story");
        m_storyStarted = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Skip();
        }
    }

    private int GetClipIndex(string clipName)
    {
        int index = 0;
        foreach (var clip in m_storyAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return index;

            index++; 
        }

        return -1;
    }

    private void Skip()
    {
        if (!m_storyStarted)
            return;

        int nextStopIndex = GetNextStopTimeIndex();
        if (nextStopIndex == -1)
            return;

        TextSpeller[] textSpellers = m_story.GetComponentsInChildren<TextSpeller>(true);
        foreach (var textSpeller in textSpellers)
            textSpeller.Skip();

        SetAnimationTime(m_stopTimes[nextStopIndex]);
    }

    private void SetAnimationTime(float time)
    {
        m_storyAnimator.Play("Story", 0, time / m_animationLength);
    }

    private float GetCurrentTime()
    {
        var stateInfo = m_storyAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime * stateInfo.length;
    }

    private int GetNextStopTimeIndex()
    {
        //float currentTime = GetCurrentNormalizedTime();
        //for (int i = m_stopTimes.Length - 1; i >= 0;  i++)
        //{
        //    if (currentTime < m_stopTimes[i])
        //        return i;
        //}

        //return -1;

        float currentTime = GetCurrentTime();

        for (int i = 0; i < m_stopTimes.Length; i++)
        {
            if (currentTime < m_stopTimes[i])
                return i;
        }

        return -1;
    }

    public void AnimationEnded()
    {
        SceneManager.LoadScene("GameplayTest");
    }

   private void SetBgTexture()
    {
        if (GameSettings.Censore)
            m_bgUiTexture.mainTexture = m_bgIos;
        else
            m_bgUiTexture.mainTexture = m_bgNormal;
    }
}

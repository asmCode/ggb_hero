using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    public Transform m_story = null;

    private Animator m_storyAnimator;
    private float[] m_stopTimes;
    private float m_animationLength = 0;

    private void Start()
    {
        m_storyAnimator = m_story.GetComponent<Animator>();
        m_animationLength = m_storyAnimator.GetCurrentAnimatorStateInfo(0).length;

        AnimationMarkers animationMarkers = m_story.GetComponent<AnimationMarkers>();
        m_stopTimes = new float[animationMarkers.Markers.Length];
        System.Array.Copy(animationMarkers.Markers, m_stopTimes, animationMarkers.Markers.Length);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Skip();
        }
    }

    private void Skip()
    {
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
}

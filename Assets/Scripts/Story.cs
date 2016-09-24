using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour
{
    public StoryController m_storyController = null;

    public void AnimationFinished()
    {
        m_storyController.AnimationEnded();
    }
}

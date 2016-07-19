using UnityEngine;
using System.Collections;

public class GrabEffect : MonoBehaviour
{
    private Animator m_animator;

    public void Play()
    {
        m_animator.Play("GrabEffect", 0, 0.0f);
    }

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        AnimationEndedGuard animEnd = GetComponent<AnimationEndedGuard>();
        animEnd.AnimationEnded += HandleAnimationEnded;
    }

    private void HandleAnimationEnded()
    {
        gameObject.SetActive(false);
    }
}

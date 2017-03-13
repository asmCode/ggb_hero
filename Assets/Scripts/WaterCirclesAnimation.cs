using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCirclesAnimation : MonoBehaviour
{
    private Animator m_animator;

    public void Play()
    {
        m_animator.SetTime(0.0f);
        m_animator.Play("WaterCircles");
    }

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }
}

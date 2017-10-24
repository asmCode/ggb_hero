using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienReturnEffect : MonoBehaviour
{
    private Animator m_animator;

    // Use this for initialization
    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Play()
    {
        m_animator.Play("AlienreturnLight", 0, 0.0f);
    }

    public void AnimEvent_Finished()
    {
        gameObject.SetActive(false);
    }
}

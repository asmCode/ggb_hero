﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCircles : MonoBehaviour
{
    public WaterCirclesAnimation m_waterCirclesAnimationPrefab;
    public float m_delay = 0.3f;

    private const int Count = 4;
    private WaterCirclesAnimation[] m_waterCirclesAnimations;
    private int m_circleAnimIndex = 0;
    private bool m_isPlaying = false;
    private float m_time = 0.0f;

    public void Play(Vector3 position)
    {
        m_isPlaying = true;
        m_time = 0.0f;

        position.y += 0.02f;

        for (int i = 0; i < Count; i++)
        {
            m_waterCirclesAnimations[i].transform.SetParent(transform);
            m_waterCirclesAnimations[i].transform.localScale = Vector3.one;
            m_waterCirclesAnimations[i].transform.position = position;
        }
    }

    public void Stop()
    {
        m_isPlaying = false;
    }

    private void Update()
    {
        CheckIfActive();

        if (!m_isPlaying)
            return;

        if (m_time <= 0.0f)
        {
            m_time = m_delay;
            PlayNextCircleAnim();
            return;
        }

        m_time -= Time.deltaTime;
    }

    private void CheckIfActive()
    {
        for (int i = 0; i < Count; i++)
        {
            if (m_waterCirclesAnimations[i].IsPlaying())
                return;
        }

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        m_waterCirclesAnimations = new WaterCirclesAnimation[Count];

        for (int i = 0; i < Count; i++)
        {
            m_waterCirclesAnimations[i] = Instantiate(m_waterCirclesAnimationPrefab);
            m_waterCirclesAnimations[i].transform.SetParent(transform);
        }
    }

    private void PlayNextCircleAnim()
    {
        m_waterCirclesAnimations[m_circleAnimIndex].Play();

        m_circleAnimIndex++;
        if (m_circleAnimIndex >= Count)
            m_circleAnimIndex = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridgeElementGenerator : MonoBehaviour
{
    private float m_minDelay = 4.0f;
    private float m_maxDelay = 8.0f;
    private float m_time;

    public void SetBurstMode()
    {
        m_minDelay = 0.2f;
        m_maxDelay = 0.4f;
        NextDelay();
    }

    public void SetNormalMode()
    {
        m_minDelay = 4.0f;
        m_maxDelay = 8.0f;
        NextDelay();
    }

    void Start()
    {
        NextDelay();
    }

    void Update()
    {
        m_time -= Time.deltaTime;
        if (m_time <= 0)
        {
            NextDelay();
            Generate();
        }
    }

    private void NextDelay()
    {
        m_time = Random.Range(m_minDelay, m_maxDelay);
    }

    private void Generate()
    {
        var element = FallingBridgeElementPool.Instance.Get();
        if (element == null)
            return;

        element.Reset();

        var pos = new Vector3(
            Random.Range(-2.2f, 2.2f),
            0,
            element.transform.localPosition.z);
        element.transform.localPosition = pos;

        element.Play();
    }
}

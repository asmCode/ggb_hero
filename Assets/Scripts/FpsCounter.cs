using UnityEngine;
using System.Collections;

public class FpsCounter : MonoBehaviour
{
    public float m_updatesPerSecond = 3.0f;

    private int m_framesCount = 0;
    private float m_lastTime = 0.0f;

    public float Fps
    {
        get;
        private set;
    }

    void Update()
    {
        if (m_lastTime == 0.0)
        {
            m_lastTime = Time.time;
        }

        m_framesCount++;

        float updateDelay = 1.0f / m_updatesPerSecond;

        float secondsFromLastUpdate = Time.time - m_lastTime;
        if (secondsFromLastUpdate >= updateDelay)
        {
            Fps = m_framesCount / secondsFromLastUpdate;
            m_lastTime = Time.time;
            m_framesCount = 0;
        }
    }
}

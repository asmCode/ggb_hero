using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterImpulse
{
    private int m_indicesCount = 0;
    private float m_power = 0;
    private float m_dampingPerStrip = 0;
    private int m_waterStripIndex = 0;
    private float m_speed = 80; // indices per second
    private int m_direction = 0;
    private float m_time = 0.0f;

    public float Power
    {
        get { return m_power; }
    }

    public WaterImpulse(int indicesCount ,float power, float speed, float dampingPerStrip, int waterStripIndex, int direction)
    {
        m_indicesCount = indicesCount;
        m_power = power;
        m_speed = speed;
        m_dampingPerStrip = dampingPerStrip;
        m_waterStripIndex = waterStripIndex;
        m_direction = direction;
        m_time = 1.0f / m_speed;
    }

    public void Update(float deltaTime)
    {
        if (m_power <= 0.0f)
            return;

        m_time += deltaTime;
    }

    public bool GetImpulse(out int index, out float power)
    {
        float timeDelay = 1.0f / m_speed;

        while (m_time >= timeDelay)
        {
            power = m_power;
            index = m_waterStripIndex;

            m_time -= timeDelay;
            m_waterStripIndex += m_direction;
            if (m_waterStripIndex < 0)
            {
                m_waterStripIndex = 1;
                m_direction = 1;
            }
            else if (m_waterStripIndex == m_indicesCount)
            {
                m_waterStripIndex = m_indicesCount - 2;
                m_direction = -1;
            }
            m_power = Mathf.MoveTowards(m_power, 0, m_dampingPerStrip);
            return true;
        }

        index = 0;
        power = 0.0f;

        return false;
    }
}

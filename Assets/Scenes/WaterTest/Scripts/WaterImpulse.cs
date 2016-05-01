using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterImpulse
{
    private float m_power = 0;
    private float m_dampingPerStrip = 0;
    private int m_waterStripIndex = 0;
    private int m_waterStripDistance = 0;
    private float m_speed = 80; // indices per second
    private int m_direction = 0;
    private float m_time = 0.0f;

    public float Power
    {
        get { return m_power; }
    }

    public WaterImpulse(float power, float speed, float dampingPerStrip, int waterStripIndex, int direction)
    {
        m_power = power;
        m_speed = speed;
        m_dampingPerStrip = dampingPerStrip;
        m_waterStripIndex = waterStripIndex;
        m_direction = direction;
        m_time = 1.0f / m_speed;
    }

    public void Update(List<WaterStrip> waterStrips)
    {
        if (m_power <= 0.0f)
            return;

        m_time += Time.deltaTime;
        float timeDelay = 1.0f / m_speed;
        while (m_time >= timeDelay)
        {
            m_time -= timeDelay;
            m_waterStripIndex += m_direction;
            m_waterStripDistance++;
            if (m_waterStripIndex < 0)
            {
                m_waterStripIndex = 1;
                m_direction = 1;
            }
            if (m_waterStripIndex == waterStrips.Count)
            {
                m_waterStripIndex = waterStrips.Count - 2;
                m_direction = -1;
            }
            m_power = Mathf.Max(m_power - m_dampingPerStrip, 0);
            waterStrips[m_waterStripIndex].GetComponent<Rigidbody2D>().AddForce(Vector2.down * m_power, ForceMode2D.Impulse);
        }
    }
}

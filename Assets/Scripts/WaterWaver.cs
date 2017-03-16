using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaver
{
    private float m_wavingOnWaterTime;

    public void Reset()
    {
        m_wavingOnWaterTime = 0.0f;
    }

    public float GetValue(float deltaTime)
    {
        m_wavingOnWaterTime += deltaTime;
        return Mathf.Sin((m_wavingOnWaterTime + Mathf.PI) * 7.0f) * 0.02f;
    }
}

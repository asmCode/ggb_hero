using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public RectBounds m_swimArea;

    public float GetWaterHeight(float xCoord, bool rand)
    {
        float noise = 0.0f;

        if (rand)
        {
            int seed = (int)(xCoord * 100.0f);
            System.Random random = new System.Random(seed);
            noise = (float)random.NextDouble() * 0.2f - 0.1f;
        }

        return m_swimArea.GetMiddle() + noise;
    }
}

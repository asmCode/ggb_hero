using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public RectBounds m_swimArea;

    public float GetWaterHeight(float xCoord)
    {
        return m_swimArea.GetMiddle();
    }
}

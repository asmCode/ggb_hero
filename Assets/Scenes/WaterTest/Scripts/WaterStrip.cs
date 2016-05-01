using UnityEngine;
using System.Collections;

public class WaterStrip : MonoBehaviour
{
    public float GetHeight()
    {
        return transform.position.y + transform.lossyScale.y / 2.0f;
    }
}

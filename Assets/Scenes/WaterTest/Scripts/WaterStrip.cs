using UnityEngine;
using System.Collections;

public class WaterStrip : MonoBehaviour
{
    public float GetHeight()
    {
        return transform.position.y + 0.4f;
    }
}

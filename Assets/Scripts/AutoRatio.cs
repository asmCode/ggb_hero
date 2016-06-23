using UnityEngine;
using System.Collections;

public class AutoRatio : MonoBehaviour
{
    void Awake()
    {
        float currentRatio = (float)Screen.width / (float)Screen.height;
        Vector3 scale = transform.localScale;
        scale.y = scale.x / currentRatio;
        transform.localScale = scale;
    }
}

using UnityEngine;
using System.Collections;

public class Shore : MonoBehaviour
{
    public RectBounds Bounds
    {
        get;
        private set;
    }

    private void Awake()
    {
        Bounds = GetComponent<RectBounds>();
    }
}

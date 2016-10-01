using UnityEngine;
using System.Collections;

public class TrailRendererSetup : MonoBehaviour
{
    private void Awake()
    {
        var trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.sortingOrder = 49;
    }
}

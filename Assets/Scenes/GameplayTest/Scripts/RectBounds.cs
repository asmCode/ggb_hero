using UnityEngine;
using System.Collections;

public class RectBounds : MonoBehaviour
{
    public float GetLeft()
    {
        return transform.position.x - transform.localScale.x / 2.0f;
    }

    public float GetRight()
    {
        return transform.position.x + transform.localScale.x / 2.0f;
    }

    public Bounds GetBounds()
    {
        return new Bounds(transform.position, transform.localScale);
    }
}

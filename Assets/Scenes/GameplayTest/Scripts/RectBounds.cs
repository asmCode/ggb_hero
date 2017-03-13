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

    public float GetTop()
    {
        return transform.position.y + transform.localScale.y / 2.0f;
    }

    public float GetBottom()
    {
        return transform.position.y - transform.localScale.y / 2.0f;
    }

    public float GetMiddle()
    {
        return transform.position.y;
    }

    public Bounds GetBounds()
    {
        return new Bounds(transform.position, transform.localScale);
    }

    public bool IsPointInside(Vector2 point)
    {
        return
            IsCoordInsideHori(point.x) &&
            IsCoordInsideVert(point.y);
    }

    public bool IsCoordInsideHori(float xCoord)
    {
        float halfWidth = transform.localScale.x / 2.0f;
        return
            xCoord >= transform.position.x - halfWidth &&
            xCoord <= transform.position.x + halfWidth;
    }

    public bool IsCoordInsideVert(float yCoord)
    {
        float halfHeight = transform.localScale.y / 2.0f;
        return
            yCoord >= transform.position.y - halfHeight &&
            yCoord <= transform.position.y + halfHeight;
    }

    private void OnDrawGizmos()
    {
        //if (Application.isPlaying)
        //    return;

        Vector3 pos = transform.position;
        Vector3 scl = transform.localScale;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos, scl);
    }
}

using UnityEngine;
using System.Collections;

public class ShoreArrow : MonoBehaviour
{
    private float m_baseY;

    private void Awake()
    {
        m_baseY = transform.position.y;
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position.y = m_baseY + Mathf.Abs(Mathf.Sin(Time.time * 6.0f)) * 0.1f;
        transform.position = position;
    }
}

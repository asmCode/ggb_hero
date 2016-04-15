using UnityEngine;
using System.Collections;

public class Walker
{
    private float m_horiDirection;
    private float m_walkingSpeed;
    private float m_baseY;
    private float m_time;
    private Transform m_walkingObject;

    public Walker(Transform walkingObject, float direction, float speed)
    {
        m_walkingObject = walkingObject;
        m_horiDirection = direction;
        m_walkingSpeed = speed;
        m_baseY = m_walkingObject.position.y;
    }

    public void Update()
    {
        m_time += Time.deltaTime;
        Vector3 position = m_walkingObject.position;
        position.x += m_walkingSpeed * m_horiDirection * Time.deltaTime;
        position.y = m_baseY + Mathf.Abs(Mathf.Sin(m_time * 15.0f)) * 0.03f;
        m_walkingObject.position = position;
    }
}

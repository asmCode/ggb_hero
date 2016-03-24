using UnityEngine;
using System.Collections;

public class SuperheroControllerAllDirections : MonoBehaviour
{
    public Superhero m_superhero;
    public Stick m_stick;

    private static readonly float m_decelaration = 20.0f;
    private static readonly float MaxSpeed = 8.0f;
    private Vector2 m_velocity;
    private float m_acceleration;

    void Update()
    {
        m_acceleration = 20.0f;
        bool isMoving = m_stick.Value != Vector2.zero;

        Vector2 moveDirection = m_stick.Value;

        if (isMoving)
        {
            m_velocity += moveDirection * m_acceleration * Time.deltaTime;
        }
        else if (m_velocity.magnitude != 0.0f)
        {
            m_acceleration = m_decelaration;

            moveDirection = m_velocity.normalized;
            moveDirection = -moveDirection * m_acceleration * Time.deltaTime;
            if (moveDirection.magnitude > m_velocity.magnitude)
                m_velocity = Vector3.zero;
            else
                m_velocity += moveDirection;
        }

        if (m_velocity.magnitude > MaxSpeed)
        {
            m_velocity.Normalize();
            m_velocity *= MaxSpeed;
        }

        float hmargin = 8.5f;
        float bottom = -4.0f;
        float top = 5.0f;

        m_velocity = m_stick.Value * MaxSpeed * Time.deltaTime * 40;

        Vector3 position = m_superhero.transform.position;
        position += new Vector3(m_velocity.x, m_velocity.y, 0) * Time.deltaTime;
        if (position.x < -hmargin)
        {
            position.x = -hmargin;
            m_velocity.x = 0;
        }
        if (position.x > hmargin)
        {
            position.x = hmargin;
            m_velocity.x = 0;
        }
        if (position.y > top)
        {
            position.y = top;
            m_velocity.y = 0;
        }
        if (position.y < bottom)
        {
            position.y = bottom;
            m_velocity.y = 0;
        }
        m_superhero.transform.position = position;
    }
}

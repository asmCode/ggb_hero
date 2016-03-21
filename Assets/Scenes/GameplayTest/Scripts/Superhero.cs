using UnityEngine;
using System.Collections;

public class Superhero : MonoBehaviour
{
    private static readonly float m_decelaration = 20.0f;
    private static readonly float MaxSpeed = 8.0f;
    private Vector3 m_velocity;
    private float m_acceleration;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_acceleration = 20.0f;
        bool isMoving = false;

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.up;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.down;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
            isMoving = true;
        }

        if (isMoving)
        {
            moveDirection.Normalize();
            m_velocity += moveDirection * m_acceleration * Time.deltaTime;
        }
        else
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

        Vector3 position = transform.position;
        position += m_velocity * Time.deltaTime;
        transform.position = position;
    }
}

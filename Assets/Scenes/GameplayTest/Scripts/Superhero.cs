using UnityEngine;
using System.Collections;

public class Superhero : MonoBehaviour
{
    public Transform m_hand;

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

        float hmargin = 8.5f;
        float bottom = -4.0f;
        float top = 5.0f;

        Vector3 position = transform.position;
        position += m_velocity * Time.deltaTime;
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
        transform.position = position;

        if (!IsFree() &&
            ((position.x < -hmargin + 1) || (position.x > hmargin - 1)))
        {
            ReleaseSuiciders();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsFree())
            return;

        Suicider suicider = other.gameObject.GetComponent<Suicider>();

        other.gameObject.transform.parent = m_hand;
        other.transform.localPosition = Vector3.zero;
        suicider.StopFalling();
    }

    private bool IsFree()
    {
        return m_hand.childCount == 0;
    }

    private void ReleaseSuiciders()
    {
        DestroyObject(m_hand.GetChild(0).gameObject);
    }
}

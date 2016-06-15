using UnityEngine;
using System.Collections;

public class Paralaxa : MonoBehaviour
{
    public Transform m_objectToFollow;

    private Vector3 m_velocity;
    private Vector3 m_basePosition;
    private static readonly Vector2 m_bounds = new Vector2(0.15f, 0.06f);

    private void Awake()
    {
        m_basePosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        //position = Vector3.SmoothDamp(position, m_objectToFollow.position, ref m_velocity, 0.1f);
        //position.z = transform.position.z;

        position.x = Mathf.Lerp(-m_bounds.x, m_bounds.x, (m_objectToFollow.position.x + 2.88874f) / (2.88874f * 2));
        position.y = Mathf.Lerp(-m_bounds.y, m_bounds.y, (m_objectToFollow.position.y + 1.04258f) / (1.04258f * 2));
        position.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, m_basePosition + position, ref m_velocity, 0.03f);
    }
}

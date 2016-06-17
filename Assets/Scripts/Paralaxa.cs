using UnityEngine;
using System.Collections;

public class Paralaxa : MonoBehaviour
{
    public Transform m_objectToFollow;
    public RectBounds m_objectToFollowBounds;

    private Vector3 m_velocity;
    private Vector3 m_basePosition;
    private static readonly Vector2 m_bounds = new Vector2(0.1f, 0.1f);

    private void Awake()
    {
        m_basePosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        //position = Vector3.SmoothDamp(position, m_objectToFollow.position, ref m_velocity, 0.1f);
        //position.z = transform.position.z;

        Bounds bounds = m_objectToFollowBounds.GetBounds();
        position.x = Mathf.Lerp(-m_bounds.x, m_bounds.x, (m_objectToFollow.position.x - bounds.min.x) / bounds.size.x);
        position.y = Mathf.Lerp(-m_bounds.y, m_bounds.y, (m_objectToFollow.position.y - bounds.min.y) / bounds.size.y);
        position.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, m_basePosition + position, ref m_velocity, 0.4f);
    }
}

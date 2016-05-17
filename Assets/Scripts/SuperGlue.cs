using UnityEngine;
using System.Collections;

public class SuperGlue : MonoBehaviour
{
    public Transform m_target;
    public Vector2 m_offset;

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position = m_target.position;
        position.x += m_offset.x;
        position.y += m_offset.y;
        transform.position = position;
    }
}

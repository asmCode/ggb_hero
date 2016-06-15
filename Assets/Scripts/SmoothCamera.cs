using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    public Transform m_objectToFollow;

    private Vector3 m_velocity;
    private Vector2 m_bounds;

    private void Start()
    {
        CameraAutoRatio cameraAutoRato = GetComponent<CameraAutoRatio>();
        //m_bounds = new Vector2(
            //cameraAutoRato.full cameraAutoRato.BaseCameraSize
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        //position = Vector3.SmoothDamp(position, m_objectToFollow.position, ref m_velocity, 0.1f);
        //position.z = transform.position.z;

        position.x = Mathf.Lerp(-m_bounds.x, m_bounds.x, (m_objectToFollow.position.x + 1.88874f) / (1.88874f * 2));
        position.y = Mathf.Lerp(-m_bounds.y, m_bounds.y, (m_objectToFollow.position.y + 0.54258f) / (0.54258f * 2));
        position.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, position, ref m_velocity, 0.03f);
    }
}

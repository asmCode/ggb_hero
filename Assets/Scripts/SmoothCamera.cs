using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    public Transform m_objectToFollow;
    public RectBounds m_objectToFollowBounds;
    public RectBounds m_cameraSafeArea;

    private Vector3 m_velocity;
    private Vector2 m_bounds;

    private void Start()
    {
        /*
        CameraAutoRatio cameraAutoRato = GetComponent<CameraAutoRatio>();
        cameraAutoRato.SetSize();
        Camera camera = GetComponent<Camera>();
        float aspect = (float)Screen.width / Screen.height;
        float camSizeDiffX = CameraAutoRatio.FullCameraSize * (4.0f / 3.0f) - camera.orthographicSize * aspect;
        float camSizeDiffY = CameraAutoRatio.FullCameraSize - camera.orthographicSize;
        m_bounds = new Vector2(camSizeDiffX, camSizeDiffY);
        */

        Camera camera = GetComponent<Camera>();
        float camWidth = camera.orthographicSize * camera.aspect;
        float camHeight = camera.orthographicSize;

        m_bounds = new Vector2();
        m_bounds.x = camWidth - m_cameraSafeArea.GetBounds().max.x;
        m_bounds.y = camHeight - m_cameraSafeArea.GetBounds().max.y;
    }

    private void Update()
    {
        Vector3 position = transform.position;
        //position = Vector3.SmoothDamp(position, m_objectToFollow.position, ref m_velocity, 0.1f);
        //position.z = transform.position.z;

        Bounds bounds = m_objectToFollowBounds.GetBounds();
        position.x = Mathf.Lerp(-m_bounds.x, m_bounds.x, (m_objectToFollow.position.x - bounds.min.x) / bounds.size.x);
        position.y = Mathf.Lerp(-m_bounds.y, m_bounds.y, (m_objectToFollow.position.y - bounds.min.y) / bounds.size.y);
        position.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, position, ref m_velocity, 0.4f);
    }
}

using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    public Transform m_objectToFollow;
    public RectBounds m_objectToFollowBounds;
    public RectBounds m_cameraSafeArea;

    private Vector3 m_velocity;
    private Vector2 m_cameraPositionConstrains;

    private void Start()
    {
        Camera camera = GetComponent<Camera>();
        float camWidth = camera.orthographicSize * camera.aspect * 2;
        float camHeight = camera.orthographicSize * 2;

        m_cameraPositionConstrains = new Vector2();
        // We are assuming, that camera is bigger than safe area
        m_cameraPositionConstrains.x = camWidth - m_cameraSafeArea.GetBounds().size.x;
        m_cameraPositionConstrains.y = camHeight - m_cameraSafeArea.GetBounds().size.y;
    }

    private void Update()
    {
        Vector3 position = transform.position;
        
        Bounds objectToFollowBounds = m_objectToFollowBounds.GetBounds();
        Bounds cameraSafeAreaBounds = m_cameraSafeArea.GetBounds();

        float maxLeftCamPos = -m_cameraPositionConstrains.x / 2 + cameraSafeAreaBounds.center.x;
        float maxRightCamPos = m_cameraPositionConstrains.x / 2 + cameraSafeAreaBounds.center.x;
        float maxBottomCamPos = -m_cameraPositionConstrains.y / 2 + cameraSafeAreaBounds.center.y;
        float maxTopCamPos = m_cameraPositionConstrains.y / 2 + cameraSafeAreaBounds.center.y;

        position.x = Mathf.Lerp(maxLeftCamPos, maxRightCamPos, Mathf.Clamp01((m_objectToFollow.position.x - objectToFollowBounds.min.x) / objectToFollowBounds.size.x));
        position.y = Mathf.Lerp(maxBottomCamPos, maxTopCamPos, Mathf.Clamp01((m_objectToFollow.position.y - objectToFollowBounds.min.y) / objectToFollowBounds.size.y));
        position.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, position, ref m_velocity, 0.4f);
    }
}

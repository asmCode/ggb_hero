using UnityEngine;
using System.Collections;

public class CameraAutoRatio : MonoBehaviour
{
    public static float FullCameraSize = 2.4f;

    private bool m_isSizeSet;
    private float m_baseCameraSize;

    public float BaseCameraSize
    {
        get
        {
            if (!m_isSizeSet)
                SetSize();

            return m_baseCameraSize;
        }
    }

    void Start()
    {
        if (!m_isSizeSet)
            SetSize();
    }

    private void SetSize()
    {
        Camera camera = GetComponent<Camera>();
        m_baseCameraSize = camera.orthographicSize;
        float baseRatio = 640.0f / 480.0f;
        float currentRatio = (float)Screen.width / (float)Screen.height;
        float ratioSratio = currentRatio / baseRatio;
        float cameraSize = m_baseCameraSize / ratioSratio;
        camera.orthographicSize = cameraSize;
        m_isSizeSet = true;
    }
}

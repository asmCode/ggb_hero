using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    public Water m_water;

    private int m_waterStripIndex;
    private bool falling = true;
    private static readonly float FallingSpeed = 0.25f;

    private void Start()
    {
        m_waterStripIndex = m_water.GetWaterStripIndex(transform.position.x);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 position = transform.position;
            position = worldPosition;
            position.z = 0.0f;
            transform.position = position;
            m_waterStripIndex = m_water.GetWaterStripIndex(transform.position.x);
            falling = true;
        }
    }

    private void LateUpdate()
    {
        float waterHeight = m_water.GetWaterHeight(m_waterStripIndex);
        Vector3 position = transform.position;

        if (falling)
        {
            position.y -= FallingSpeed * Time.deltaTime;

            if (waterHeight >= position.y - 0.01f)
            {
                falling = false;
                m_water.Impulse(m_waterStripIndex, 1.3f, 0.0f);
                position.y = waterHeight;
            }
        }
        else
        {
            position.y = waterHeight;
        }

        transform.position = position;
    }
}

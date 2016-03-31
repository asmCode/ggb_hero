using UnityEngine;
using System.Collections;

public class SuiControllerWalkOnBridge : SuiController
{
    private float m_horiDirection;
    private float m_walkingSpeed;
    private float m_jumpAfter;
    private float m_baseY;
    private float m_time;

    public SuiControllerWalkOnBridge(Suicider sui, Vector2 startPosition, float jumpAfter) : base(sui)
    {
        sui.transform.position = startPosition;
        m_horiDirection = (startPosition.x < 0.0f) ? 1.0f : -1.0f;
        m_walkingSpeed = Random.Range(0.5f, 1.5f);
        m_jumpAfter = jumpAfter;
        m_baseY = startPosition.y;
    }

    public override void UpdateSui()
    {
        m_time += Time.deltaTime;
        Vector3 position = m_sui.transform.position;
        position.x += m_walkingSpeed * m_horiDirection * Time.deltaTime;
        position.y = m_baseY + Mathf.Abs(Mathf.Sin(m_time * 15.0f)) * 0.15f;
        m_sui.transform.position = position;

        if ((m_horiDirection == 1.0f && position.x >= m_jumpAfter) ||
            (m_horiDirection == -1.0f && position.x <= m_jumpAfter))
        {
            m_sui.SetController(new SuiControllerFalling(m_sui, position));
        }
    }
}

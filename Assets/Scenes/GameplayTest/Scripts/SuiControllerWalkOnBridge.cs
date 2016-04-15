using UnityEngine;
using System.Collections;

public class SuiControllerWalkOnBridge : SuiController
{
    private float m_horiDirection;
    private float m_jumpAfter;
    private Walker m_walker;

    public SuiControllerWalkOnBridge(Suicider sui, Vector2 startPosition, float jumpAfter) : base(sui)
    {
        sui.transform.position = startPosition;
        m_jumpAfter = jumpAfter;
        m_horiDirection = (startPosition.x < 0.0f) ? 1.0f : -1.0f;
        float speed = Random.Range(0.2f, 0.6f);
        m_walker = new Walker(sui.transform, m_horiDirection, speed);
    }

    public override void UpdateSui()
    {
        m_walker.Update();

        if ((m_horiDirection == 1.0f && m_sui.transform.position.x >= m_jumpAfter) ||
            (m_horiDirection == -1.0f && m_sui.transform.position.x <= m_jumpAfter))
        {
            m_sui.SetController(new SuiControllerPreparingForJump(m_sui));
        }
    }
}

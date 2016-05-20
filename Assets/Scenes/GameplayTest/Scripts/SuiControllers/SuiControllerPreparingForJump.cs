using UnityEngine;
using System.Collections;

public class SuiControllerPreparingForJump : SuiController
{
    private static readonly Vector2 JumpTimeBounds = new Vector2(2.0f, 4.0f);
    
    private float m_jumpAfterTime;
    private float m_time;
    private Vector2 m_basePosition;
    private float m_jumpOverFenceProgress;

    public SuiControllerPreparingForJump(Suicider sui) : base(sui)
    {
        m_basePosition = sui.transform.position;
        m_jumpAfterTime = Random.Range(JumpTimeBounds.x, JumpTimeBounds.y);
    }

    public override void UpdateSui()
    {
        m_time += Time.deltaTime;
        if (m_time < 1.0f)
            return;

        if (m_jumpOverFenceProgress < 1.0f)
        {
            m_jumpOverFenceProgress = Mathf.Min(m_jumpOverFenceProgress + Time.deltaTime * 1.0f, 1.0f);
            Vector2 position = m_basePosition;
            position.y += Mathf.Sin(m_jumpOverFenceProgress * Mathf.PI) * 0.08f;
            m_sui.transform.position = position;
            return;
        }

        if (m_time >= m_jumpAfterTime)
        {
            m_sui.SetController(new SuiControllerFalling(m_sui));
        }
    }
}

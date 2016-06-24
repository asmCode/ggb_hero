using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerPreparingForJump : SuiController
{
    private static readonly Vector2 JumpTimeBounds = new Vector2(2.0f, 4.0f);

    private float m_jumpAfterTime;
    private float m_time;
    private Vector2 m_basePosition;
    private float m_jumpOverFenceProgress;
    private bool m_changedSpriteOrder;

    public static List<Suicider> Suiciders
    {
        get;
        private set;
    }

    static SuiControllerPreparingForJump()
    {
        Suiciders = new List<Suicider>();
    }

    public static void Reset()
    {
        Suiciders.Clear();
    }

    public SuiControllerPreparingForJump(Suicider sui) : base(sui)
    {
        Suiciders.Add(sui);

        m_basePosition = sui.transform.position;
        m_jumpAfterTime = Random.Range(JumpTimeBounds.x, JumpTimeBounds.y);
        sui.Dude.SetBobyPartsKinematic(true);
        sui.DudeAnimator.PrepareToJump();
    }

    public override void UpdateSui()
    {
        m_time += Time.deltaTime;
        if (m_time < 1.0f)
            return;

        if (m_jumpOverFenceProgress < 1.0f)
        {
            if (m_jumpOverFenceProgress >= 0.5f && !m_changedSpriteOrder)
            {
                m_sui.SortOrder = m_sui.SortOrder + 5;
                m_changedSpriteOrder = true;
            }

            m_jumpOverFenceProgress = Mathf.Min(m_jumpOverFenceProgress + Time.deltaTime * 1.0f, 1.0f);
            Vector2 position = m_basePosition;
            position.y += Mathf.Sin(m_jumpOverFenceProgress * Mathf.PI) * 0.15f;
            m_sui.transform.position = position;
            return;
        }

        if (m_time >= m_jumpAfterTime)
        {
            m_sui.SetController(new SuiControllerFalling(m_sui));
        }
    }

    public override void Leaving()
    {
        if (Suiciders.Count == 0)
        {
            Debug.LogError("Logic error");
            return;
        }

        Suiciders.Remove(m_sui);
    }
}

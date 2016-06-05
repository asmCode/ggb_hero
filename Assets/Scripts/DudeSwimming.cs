using UnityEngine;
using System.Collections;

public class DudeSwimming : DudeAnimationClip
{
    private float m_time;
    private float m_direction;

    public DudeSwimming(DudeAnimator dudeAnimator, float direction) : base(dudeAnimator)
    {
        m_direction = direction;
    }

    public override void Update()
    {
        m_time += Time.deltaTime;

        DudeAnimator.BodyAngleTarget = 70 * m_direction;

        DudeAnimator.HandLeftAngleTarget = m_time * 300.0f * m_direction;
        DudeAnimator.HandRightAngleTarget = m_time * 300.0f * m_direction;

        DudeAnimator.LegLeftAngleTarget = Mathf.Sin(m_time * 20.0f) * 20.0f * m_direction;
        DudeAnimator.LegRightAngleTarget = Mathf.Sin(m_time * 20.0f + 3.1415f) * 20.0f * m_direction;
    }
}

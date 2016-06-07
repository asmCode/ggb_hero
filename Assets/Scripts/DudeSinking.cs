using UnityEngine;
using System.Collections;

public class DudeSinking : DudeAnimationClip
{
    private float m_time;
   
    public DudeSinking(DudeAnimator dudeAnimator) : base(dudeAnimator)
    {
    }

    public override void Update()
    {
        m_time += Time.deltaTime;

        DudeAnimator.BodyAngleTarget = 0;

        DudeAnimator.HandRightAngleTarget = Mathf.Sin(m_time * 10.0f) * 40.0f + 30.0f;
        DudeAnimator.HandLeftAngleTarget = Mathf.Sin(m_time * 10.0f + 3.1415f) * 40.0f - 30.0f;

        DudeAnimator.LegRightAngleTarget = Mathf.Sin(m_time * 10.0f) * 15.0f;
        DudeAnimator.LegLeftAngleTarget = Mathf.Sin(m_time * 10.0f + 3.1415f) * 15.0f;
    }
}

using UnityEngine;
using System.Collections;

public class DudeFalling : DudeAnimationClip
{
    private float m_time;
   
    public DudeFalling(DudeAnimator dudeAnimator) : base(dudeAnimator)
    {
    }

    public override void Update()
    {
        m_time += Time.deltaTime;

        DudeAnimator.BodyAngleTarget = 0;

        DudeAnimator.HandRightAngleTarget = Mathf.Sin(m_time * 40.0f) * 20.0f + 70.0f;
        DudeAnimator.HandLeftAngleTarget = Mathf.Sin(m_time * 40.0f + 3.1415f) * 20.0f - 70.0f;

        DudeAnimator.LegRightAngleTarget = Mathf.Sin(m_time * 30.0f) * 15.0f + 40;
        DudeAnimator.LegLeftAngleTarget = Mathf.Sin(m_time * 30.0f + 3.1415f) * 15.0f - 40;
    }
}

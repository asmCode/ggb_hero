using UnityEngine;
using System.Collections;

public class DudePreparingToJump : DudeAnimationClip
{
    private float m_time;
   
    public DudePreparingToJump(DudeAnimator dudeAnimator) : base(dudeAnimator)
    {
    }

    public override void Update()
    {
        m_time += Time.deltaTime;

        DudeAnimator.BodyAngleTarget = 0;

        DudeAnimator.HandRightAngleTarget = -30.0f;
        DudeAnimator.HandLeftAngleTarget = 30.0f;

        DudeAnimator.LegRightAngleTarget = 10.0f;
        DudeAnimator.LegLeftAngleTarget = -10.0f;
    }
}

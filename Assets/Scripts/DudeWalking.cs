using UnityEngine;
using System.Collections;

public class DudeWalking : DudeAnimationClip
{
    private float m_time;
   
    public DudeWalking(DudeAnimator dudeAnimator) : base(dudeAnimator)
    {
    }

    public override void Update()
    {
        m_time += Time.deltaTime;

        //DudeAnimator.HandLeftAngleTarget = m_time * 600.0f * m_direction;
        //DudeAnimator.HandRightAngleTarget = m_time * 600.0f * m_direction;


        DudeAnimator.BodyAngleTarget = 0;

        DudeAnimator.HandRightAngleTarget = Mathf.Sin(m_time * 20.0f) * 20.0f - 60.0f;
        DudeAnimator.HandLeftAngleTarget = Mathf.Sin(m_time * 20.0f + 3.1415f) * 20.0f + 60.0f;

        DudeAnimator.LegRightAngleTarget = Mathf.Sin(m_time * 20.0f) * 20.0f;
        DudeAnimator.LegLeftAngleTarget = Mathf.Sin(m_time * 20.0f + 3.1415f) * 20.0f;

        //DudeAnimator.LegLeftAngleTarget = 0;
        //DudeAnimator.LegRightAngleTarget = 0;
    }
}

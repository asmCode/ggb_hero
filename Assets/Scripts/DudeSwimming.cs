using UnityEngine;
using System.Collections;

public class DudeSwimming : DudeAnimationClip
{
    private Superhero m_superhero;
    private float m_time;

    public DudeSwimming(DudeAnimator dudeAnimator) : base(dudeAnimator)
    {
        m_superhero = dudeAnimator.GetComponent<Superhero>();
    }

    public override void Update()
    {
        m_time += Time.deltaTime;

        float direction = Mathf.Sign(m_superhero.Velocity.x);

        if (m_superhero.Velocity == Vector2.zero)
        {
            DudeAnimator.HandRightAngleTarget = -20.0f;
            DudeAnimator.HandLeftAngleTarget = 20.0f;
            DudeAnimator.BodyAngleTarget = 0;
            DudeAnimator.LegLeftAngleTarget = Mathf.Sin(m_time * 10.0f) * 20.0f;
            DudeAnimator.LegRightAngleTarget = Mathf.Sin(m_time * 10.0f + 3.1415f) * 20.0f;
            return;
        }

        DudeAnimator.BodyAngleTarget = 85 * -direction;
        DudeAnimator.LegLeftAngleTarget = Mathf.Sin(m_time * 20.0f) * 20.0f;
        DudeAnimator.LegRightAngleTarget = Mathf.Sin(m_time * 20.0f + 3.1415f) * 20.0f;
        DudeAnimator.HandLeftAngleTarget = m_time * 600.0f * -direction;
        DudeAnimator.HandRightAngleTarget = m_time * 600.0f * -direction;
    }
}

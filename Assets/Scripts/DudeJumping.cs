using UnityEngine;
using System.Collections;

public class DudeJumping : DudeAnimationClip
{
    private Superhero m_superhero;
    private float m_time;
   
    public DudeJumping(DudeAnimator dudeAnimator) : base(dudeAnimator)
    {
        m_superhero = dudeAnimator.GetComponent<Superhero>();
    }

    public override void Update()
    {
        m_time += Time.deltaTime;

        //DudeAnimator.HandLeftAngleTarget = m_time * 600.0f * m_direction;
        //DudeAnimator.HandRightAngleTarget = m_time * 600.0f * m_direction;

        float yVelocity = m_superhero.Velocity.y;
        float xVelocity = m_superhero.Velocity.x;

        if (m_superhero.Velocity == Vector2.zero)
        {
            DudeAnimator.HandRightAngleTarget = -70.0f;
            DudeAnimator.HandLeftAngleTarget = 70.0f;
            DudeAnimator.BodyAngleTarget = 0;
            DudeAnimator.LegLeftAngleTarget = 0;
            DudeAnimator.LegRightAngleTarget = 0;
            return;
        }

        float yVelocityNormalized = Mathf.Clamp01(1.0f - yVelocity / 2);

        float direction = Mathf.Sign(xVelocity);

        DudeAnimator.BodyAngleTarget = 15 * -direction * Mathf.Clamp(yVelocity, -1.0f, 1.0f);

        DudeAnimator.HandRightAngleTarget = 80 * /*direction **/ Mathf.Clamp(yVelocity, -1, 1.2f);
        DudeAnimator.HandLeftAngleTarget = -80 * /*direction **/ Mathf.Clamp(yVelocity, -1, 1.2f);

        DudeAnimator.LegLeftAngleTarget = Mathf.SmoothStep(-30.0f, 0.0f, yVelocityNormalized);
        DudeAnimator.LegRightAngleTarget = Mathf.SmoothStep(30.0f, 0.0f, yVelocityNormalized);
        //DudeAnimator.LegLeftAngleTarget = 0;
        //DudeAnimator.LegRightAngleTarget = 0;
    }
}

using UnityEngine;
using System.Collections;

public class DudeJumping : DudeAnimationClip
{
    private Superhero m_superhero;
    private float m_time;
    private float m_direction;

    public DudeJumping(DudeAnimator dudeAnimator, float direction) : base(dudeAnimator)
    {
        m_direction = direction;
        m_superhero = dudeAnimator.GetComponent<Superhero>();
    }

    public override void Update()
    {
        m_time += Time.deltaTime;

        //DudeAnimator.HandLeftAngleTarget = m_time * 600.0f * m_direction;
        //DudeAnimator.HandRightAngleTarget = m_time * 600.0f * m_direction;

        float yVelocity = m_superhero.Velocity.y;
        float xVelocity = m_superhero.Velocity.x;
        float yVelocityNormalized = Mathf.Clamp01(1.0f - yVelocity / 2);

        float direction = Mathf.Sign(xVelocity);

        DudeAnimator.BodyAngleTarget = 15 * -direction * Mathf.Clamp(yVelocity, -1.0f, 1.0f);

        DudeAnimator.HandRightAngleTarget = 80 * /*direction **/ Mathf.Clamp(yVelocity, -1, 1.2f);
        DudeAnimator.HandLeftAngleTarget = -80 * /*direction **/ Mathf.Clamp(yVelocity, -1, 1.2f);

        DudeAnimator.LegLeftAngleTarget = Mathf.SmoothStep(-80.0f, 0.0f, yVelocityNormalized);
        DudeAnimator.LegRightAngleTarget = Mathf.SmoothStep(80.0f, 0.0f, yVelocityNormalized);
    }
}

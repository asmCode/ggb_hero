using UnityEngine;
using System.Collections;
using System;

public class DudeAnimator : MonoBehaviour
{
    #region Transform
    public BodyPartPivot m_bodyPivot;
    public BodyPartPivot m_handPivotLeft;
    public BodyPartPivot m_handPivotRight;
    public BodyPartPivot m_legPivotLeft;
    public BodyPartPivot m_legPivotRight;
    #endregion

    private float m_bodyAngle;
    private float m_bodyAngleSpeed;

    private float m_handLeftAngle;
    private float m_handLeftAngleSpeed;
    private float m_handRightAngle;
    private float m_handRightAngleSpeed;
    private float m_legLeftAngle;
    private float m_legLeftAngleSpeed;
    private float m_legRightAngle;
    private float m_legRightAngleSpeed;

    public float BodyAngleTarget { get; set; }
    public float HandLeftAngleTarget { get; set; }
    public float HandRightAngleTarget { get; set; }

    public float LegLeftAngleTarget { get; set; }
    public float LegRightAngleTarget { get; set; }

    private DudeAnimationClip m_clip;

    private void Awake()
    {
        //SetupPivots();
    }

    public void Reset()
    {
        m_handPivotLeft.Reset();
        m_handPivotRight.Reset();
        m_legPivotLeft.Reset();
        m_legPivotRight.Reset();
    }

    public void ClearClip()
    {
        m_clip = null;

        m_handLeftAngleSpeed = 0.0f;
        m_handRightAngleSpeed = 0.0f;
        m_legLeftAngle = 0.0f;
        m_legLeftAngleSpeed = 0.0f;
        m_legRightAngle = 0.0f;
        m_legRightAngleSpeed = 0.0f;
    }
    
    public void Swim()
    {
        SetupPivots();
        m_clip = new DudeSwimming(this);
    }

    public void PrepareToJump()
    {
        SetupPivots();
        m_clip = new DudePreparingToJump(this);
    }

    public void Sink()
    {
        SetupPivots();
        m_clip = new DudeSinking(this);
    }

    public void Jump()
    {
        SetupPivots();
        m_clip = new DudeJumping(this);
    }

    public void Walk()
    {
        SetupPivots();
        m_clip = new DudeWalking(this);
    }

    public void Fall()
    {
        SetupPivots();
        m_clip = new DudeFalling(this);
    }

    //private void FixedUpdate()
    private void Update()
    {
        SetupPivot(m_bodyPivot, out m_bodyAngle);

        if (m_clip == null)
            return;

        m_clip.Update();

        Smooth(ref m_bodyAngle, BodyAngleTarget, ref m_bodyAngleSpeed, m_bodyPivot);
        Smooth(ref m_handLeftAngle, HandLeftAngleTarget, ref m_handLeftAngleSpeed, m_handPivotLeft);
        Smooth(ref m_handRightAngle, HandRightAngleTarget, ref m_handRightAngleSpeed, m_handPivotRight);
        Smooth(ref m_legLeftAngle, LegLeftAngleTarget, ref m_legLeftAngleSpeed, m_legPivotLeft);
        Smooth(ref m_legRightAngle, LegRightAngleTarget, ref m_legRightAngleSpeed, m_legPivotRight);
    }

    private void Smooth(ref float angle, float angleTarget, ref float speed, BodyPartPivot tr)
    {
        if (float.IsNaN(speed))
        {
            speed = 0.0f;
        }

        if (!tr.BodyPart.Rigidbody.isKinematic)
            return;

        float smoothTime = 0.05f;
        angle = Mathf.SmoothDampAngle(angle, angleTarget, ref speed, smoothTime);
        tr.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetupPivots()
    {
        SetupPivot(m_bodyPivot, out m_bodyAngle);
        SetupPivot(m_handPivotLeft, out m_handLeftAngle);
        SetupPivot(m_handPivotRight, out m_handRightAngle);
        SetupPivot(m_legPivotLeft, out m_legLeftAngle);
        SetupPivot(m_legPivotRight, out m_legRightAngle);
    }

    private static void SetupPivot(BodyPartPivot pivot, out float angle)
    {
        BodyPart bodyPart = pivot.BodyPart;
        if (!bodyPart.Rigidbody.isKinematic)
        {
            angle = 0.0f;
            return;
        }
        Transform childPivot = bodyPart.ChildPivot;
        Vector3 pos = bodyPart.transform.position;
        Quaternion rot = bodyPart.transform.rotation;
        pivot.transform.position = childPivot.position;
        pivot.transform.rotation = childPivot.rotation;
        bodyPart.transform.position = pos;
        bodyPart.transform.rotation = rot;

        angle = Vector2.Angle(Vector2.right, pivot.transform.right);
        if (pivot.transform.right.y < 0.0f)
            angle = -angle;
    }
}

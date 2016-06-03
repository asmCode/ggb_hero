using UnityEngine;
using System.Collections;

public class DudeAnimator : MonoBehaviour
{
    #region Transform
    public Transform m_bodyPivot;
    public Transform m_handPivotLeft;
    public Transform m_handPivotRight;
    public Transform m_legPivotLeft;
    public Transform m_legPivotRight;
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

    public void SwimLeft()
    {
        m_clip = new DudeSwimming(this, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SwimLeft();

        if (m_clip == null)
            return;

        m_clip.Update();

        Smooth(ref m_bodyAngle, BodyAngleTarget, ref m_bodyAngleSpeed, m_bodyPivot);
        Smooth(ref m_handLeftAngle, HandLeftAngleTarget, ref m_handLeftAngleSpeed, m_handPivotLeft);
        Smooth(ref m_handRightAngle, HandRightAngleTarget, ref m_handRightAngleSpeed, m_handPivotRight);
        Smooth(ref m_legLeftAngle, LegLeftAngleTarget, ref m_legLeftAngleSpeed, m_legPivotLeft);
        Smooth(ref m_legRightAngle, LegRightAngleTarget, ref m_legRightAngleSpeed, m_legPivotRight);
    }

    private void Smooth(ref float angle, float angleTarget, ref float speed, Transform tr)
    {
        angle = Mathf.SmoothDampAngle(angle, angleTarget, ref speed, 0.08f);
        tr.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

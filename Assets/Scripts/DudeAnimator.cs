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

    private void Awake()
    {
        //SetupPivots();
    }

    public void ClearClip()
    {
        m_clip = null;
    }

    public void SwimLeft()
    {
        SetupPivots();
        m_clip = new DudeSwimming(this, 1);
    }

    public void SwimRight()
    {
        SetupPivots();
        m_clip = new DudeSwimming(this, -1);
    }

    public void Jump()
    {
        SetupPivots();
        m_clip = new DudeJumping(this, -1);
    }

    private void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    SwimLeft();
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //    SwimRight();

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

    private void Smooth(ref float angle, float angleTarget, ref float speed, Transform tr)
    {
        if (!tr.GetChild(0).GetComponent<Rigidbody2D>().isKinematic)
            return;

        float smoothTime = 0.05f;
        angle = Mathf.SmoothDampAngle(angle, angleTarget, ref speed, smoothTime);
        tr.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetupPivots()
    {
        SetupPivot(m_bodyPivot, out m_bodyAngle);
        SetupPivot(m_handPivotLeft, out m_handLeftAngle);
        SetupPivot(m_handPivotRight, out m_handRightAngle);
        SetupPivot(m_legPivotLeft, out m_legLeftAngle);
        SetupPivot(m_legPivotRight, out m_legRightAngle);
    }

    private static void SetupPivot(Transform pivot, out float angle)
    {
        Transform bodyPart = pivot.GetChild(0);
        Transform childPivot = bodyPart.FindChild("ChildPivot");
        Vector3 pos = bodyPart.position;
        Quaternion rot = bodyPart.rotation;
        pivot.position = childPivot.position;
        pivot.rotation = childPivot.rotation;
        bodyPart.position = pos;
        bodyPart.rotation = rot;

        angle = Vector2.Angle(Vector2.right, pivot.right);
        if (pivot.right.y < 0.0f)
            angle = -angle;
    }
}

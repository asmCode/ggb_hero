using UnityEngine;
using System.Collections;

public class Plugable : MonoBehaviour
{
    private HingeJoint2D m_joint;
    private Rigidbody2D m_rigibody;
    private Plugable m_target;
    private Vector2 m_connectedAnchorPosition;
    private Vector2 m_connectedAnchorPositionVelocity;

    public Rigidbody2D Rigibody
    {
        get { return m_rigibody; }
    }

    public HingeJoint2D Joint
    {
        get { return m_joint; }
    }

    //public bool IsPlugged
    //{
    //    //get { return m_target != null ||  }
    //}

    public void PlugTo(Plugable plugable)
    {
        m_target = plugable;
        m_joint.connectedBody = plugable.Rigibody;
        m_joint.enabled = true;
        m_connectedAnchorPosition = TransformLocalAnchorToTarget(m_joint.anchor);
        m_connectedAnchorPositionVelocity = Vector2.zero;
    }

    public void Unplug()
    {
        m_target = null;
        m_joint.connectedBody = null;
        m_joint.connectedAnchor = Vector2.zero;
        m_joint.enabled = false;
    }

    private void Awake()
    {
        m_rigibody = GetComponent<Rigidbody2D>();
        HingeJoint2D[] joints = GetComponents<HingeJoint2D>();
        if (joints == null || joints.Length != 2)
        {
            Debug.LogError("Plugable requires 2 hinge joints 2d");
            return;
        }
        m_joint = joints[1];
    }

    private void Update()
    {
        if (m_target == null)
            return;

        m_connectedAnchorPosition = Vector2.SmoothDamp(
            m_connectedAnchorPosition, m_target.Joint.anchor, ref m_connectedAnchorPositionVelocity, 0.05f);
        m_joint.connectedAnchor = m_connectedAnchorPosition;
    }

    private Vector2 TransformLocalAnchorToTarget(Vector2 anchor)
    {
        Vector2 worldAnchor = transform.TransformPoint(anchor);
        return m_target.transform.InverseTransformPoint(worldAnchor);
    }
}

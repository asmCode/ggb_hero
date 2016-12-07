using UnityEngine;
using System.Collections;

public class Plug : MonoBehaviour
{
    public Socket m_correspondingSocket;

    private Vector2 m_connectedAnchorPosition;
    private Vector2 m_connectedAnchorPositionVelocity;

    public HingeJoint2D Joint
    {
        get;
        private set;
    }

    public Socket ConnectedSocket
    {
        get; set;
    }

    public bool IsFree
    {
        get
        {
            return
                ConnectedSocket == null &&
                (m_correspondingSocket == null || (m_correspondingSocket != null && m_correspondingSocket.ConnectedPlug == null));
        }
    }

    public void PlugIn(Socket socket)
    {
        if (!IsFree || !socket.IsFree)
        {
            Debug.LogError("socket and plug must be free");
            return;
        }

        ConnectedSocket = socket;
        socket.ConnectedPlug = this;

        Joint.connectedBody = socket.Rigidbody;
        m_connectedAnchorPosition = ConnectedSocket.transform.InverseTransformPoint(transform.position);
        m_connectedAnchorPositionVelocity = Vector2.zero;
        Joint.enabled = true;
    }

    public void PlugOut()
    {
        if (ConnectedSocket == null)
            return;

        Joint.connectedBody = null;
        Joint.enabled = false;

        ConnectedSocket.ConnectedPlug = null;
        ConnectedSocket = null;
    }

    private void Awake()
    {
        if (transform.parent == null)
        {
            Debug.LogError("Plug needs parent with HingeJoint2D.");
            return;
        }

        Joint = transform.parent.GetComponent<HingeJoint2D>();
        if (Joint == null)
        {
            Debug.LogError("Plug needs parent with HingeJoint2D.");
            return;
        }

        Joint.anchor = transform.localPosition;
    }

    private void Update()
    {
        if (ConnectedSocket == null)
            return;

        m_connectedAnchorPosition = Vector2.SmoothDamp(
            m_connectedAnchorPosition, ConnectedSocket.transform.localPosition, ref m_connectedAnchorPositionVelocity, 0.05f, Mathf.Infinity, Time.deltaTime);
        Joint.connectedAnchor = m_connectedAnchorPosition;
    }
}

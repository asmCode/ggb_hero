using UnityEngine;
using System.Collections;

public class Socket : MonoBehaviour
{
    public Plug m_correspondingPlug;

    public Rigidbody2D Rigidbody
    {
        get;
        private set;
    }

    public Plug ConnectedPlug
    {
        get; set;
    }

    public bool IsFree
    {
        get
        {
            return
                ConnectedPlug == null &&
                (m_correspondingPlug == null || (m_correspondingPlug != null && m_correspondingPlug.ConnectedSocket == null));
        }
    }

    private void Awake()
    {
        if (transform.parent == null)
        {
            Debug.LogError("Socket needs parent with Rigibody2D.");
            return;
        }

        Rigidbody = transform.parent.GetComponent<Rigidbody2D>();
        if (Rigidbody == null)
        {
            Debug.LogError("Socket needs parent with Rigibody2D.");
            return;
        }
    }
}

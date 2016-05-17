using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
    private Socket m_socket;
    private Plug m_plug;

    public bool HasSocket
    {
        get { return m_socket != null; }
    }

    public bool HasPlug
    {
        get { return m_plug != null; }
    }

    public void ConnectTo(Node node)
    {
        if (!HasPlug || !node.HasSocket)
        {
            Debug.LogError("Wrong node combination.");
            return;
        }

        m_plug.PlugIn(node.m_socket);
    }

    public void Disconnect()
    {
        if (!HasPlug || !m_plug.IsFree)
            return;

        m_plug.PlugOut();
    }

    private void Awake()
    {
        m_socket = GetComponentInChildren<Socket>();
        m_plug = GetComponentInChildren<Plug>();
    }
}

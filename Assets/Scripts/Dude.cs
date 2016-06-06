using UnityEngine;
using System.Collections;

public class Dude : MonoBehaviour
{
    public Socket[] m_sockets;
    public Plug[] m_plugs;

    private Node[] m_bodyParts = new Node[4];

    public void SetBobyPartsKinematic(bool isKinematic)
    {
        if (m_bodyParts == null)
            return;

        foreach (var item in m_bodyParts)
            item.Rigidbody.isKinematic = isKinematic;
    }

    public bool IsConnected(BodyPartType bodyPartType)
    {
        return m_bodyParts[(int)bodyPartType].IsConnected();
    }

    public void SetBobyPartKinematic(BodyPartType bodyPartType, bool isKinematic)
    {
        m_bodyParts[(int)bodyPartType].Rigidbody.isKinematic = isKinematic;
    }

    public void PlugIn(Dude sourceDude)
    {
        Plug plug = GetRandomPlug();
        Socket socket = sourceDude.GetRandomSocket();
        if (plug == null || socket == null)
            return;

        plug.PlugIn(socket);
    }

    public void PlugOut()
    {
        Plug connectedPlug = GetConnectedPlug();
        if (connectedPlug == null)
            return;

        connectedPlug.PlugOut();
    }

    private Plug GetRandomPlug()
    {
        Plug[] freePlugs = System.Array.FindAll(m_plugs, t => t.IsFree);
        if (freePlugs == null || freePlugs.Length == 0)
            return null;

        return freePlugs[Random.Range(0, freePlugs.Length)];
    }

    private Socket GetRandomSocket()
    {
        Socket[] freeSockets = System.Array.FindAll(m_sockets, t => t.IsFree);
        if (freeSockets == null || freeSockets.Length == 0)
            return null;

        return freeSockets[Random.Range(0, freeSockets.Length)];
    }

    private Plug GetConnectedPlug()
    {
        return System.Array.Find(m_plugs, t => t.ConnectedSocket != null);
    }

    private void Awake()
    {
        m_bodyParts[(int)BodyPartType.HandLeft] = transform.parent.Find("HandLeftPivot/HandLeft").GetComponent<Node>();
        m_bodyParts[(int)BodyPartType.HandRight] = transform.parent.Find("HandRightPivot/HandRight").GetComponent<Node>();
        m_bodyParts[(int)BodyPartType.LegLeft] = transform.parent.Find("LegLeftPivot/LegLeft").GetComponent<Node>();
        m_bodyParts[(int)BodyPartType.LegRight] = transform.parent.Find("LegRightPivot/LegRight").GetComponent<Node>();
    }
}

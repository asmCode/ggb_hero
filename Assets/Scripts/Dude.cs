using UnityEngine;
using System.Collections;

public class Dude : MonoBehaviour
{
    public Socket[] m_sockets;
    public Plug[] m_plugs;

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
}

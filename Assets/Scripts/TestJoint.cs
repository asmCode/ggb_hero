using UnityEngine;
using System.Collections;

public class TestJoint : MonoBehaviour
{
    public Dude plug;
    public Dude socket;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            plug.PlugIn(socket);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            plug.PlugOut();
        }
    }
}

using UnityEngine;
using System.Collections;

public class Stick : MonoBehaviour
{
    public Vector2 Value
    {
        get;
        protected set;
    }

    public Vector2 Origin
    {
        get;
        protected set;
    }

    public float Range
    {
        get { return 0.2f; }
    }
}

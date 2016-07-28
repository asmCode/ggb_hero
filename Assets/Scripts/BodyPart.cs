using UnityEngine;
using System.Collections;

public class BodyPart : MonoBehaviour
{
    public Transform ChildPivot
    {
        get;
        private set;
    }

    public Rigidbody2D Rigidbody
    {
        get;
        private set;
    }

    private void Awake()
    {
        ChildPivot = transform.Find("ChildPivot");
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}

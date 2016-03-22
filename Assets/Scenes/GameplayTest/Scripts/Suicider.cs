using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    private static readonly float FallingSpeed = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.y -= FallingSpeed * Time.deltaTime;
        transform.position = position;
    }
}

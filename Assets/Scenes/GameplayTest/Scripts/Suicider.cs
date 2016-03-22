using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    private static readonly float FallingSpeed = 1.0f;
    private bool m_isFalling = true;

    public void StopFalling()
    {
        m_isFalling = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFalling)
        {
            Vector3 position = transform.position;
            position.y -= FallingSpeed * Time.deltaTime;
            transform.position = position;
        }
    }
}

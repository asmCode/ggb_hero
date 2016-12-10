using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridgeElement : MonoBehaviour
{
    public ParticleSystem m_fire;
    public ParticleSystem m_smoke;
    public Water m_water;

    private float m_rotationSpeed;
    private float m_fallingSpeed;

    void Awake()
    {
        Invoke("Play", 7.0f);
    }

    public void Play()
    {
        SetFireEnabled(true);
        Invoke("StartFalling", 1.5f);
    }

    public void Reset()
    {
        transform.rotation = Quaternion.identity;
        m_fire.Stop();
        m_fire.Clear();
        m_smoke.Stop();
        m_smoke.Clear();
    }

    private void Update()
    {
        var position = transform.position;
        position.y -= m_fallingSpeed * Time.deltaTime;
        transform.position = position;

        var rotation = transform.rotation;
        rotation = rotation * Quaternion.AngleAxis(m_rotationSpeed * Time.deltaTime, Vector3.forward);
        transform.rotation = rotation;
    }

    private void StartFalling()
    {
        m_fallingSpeed = Random.Range(0.4f, 0.6f);
        //m_rotationSpeed = Random.Range(-10.0f, 10.0f);
        m_rotationSpeed = -45.0f;
    }

    private void SetFireEnabled(bool emabled)
    {
        if (emabled)
        {
            m_fire.Play();
            m_smoke.Play();
        }
        else
        {
            m_fire.Stop();
            m_smoke.Stop();
        }
    }

    private void SetRotation(bool rotation)
    {

    }

    private void SetFallingSpeed(float speed)
    {

    }
}

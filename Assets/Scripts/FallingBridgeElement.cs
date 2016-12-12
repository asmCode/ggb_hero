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
    private bool m_isOnWater;

    public void Play()
    {
        SetFireEnabled(true);
        Invoke("StartFalling", 1.5f);

        var randPos = new Vector3(
            Random.Range(-0.13f, 0.13f),
            Random.Range(-0.13f, 0.13f),
            0);

        m_fire.transform.localPosition = randPos;
        m_smoke.transform.localPosition = randPos;
    }

    public void Reset()
    {
        transform.rotation = Quaternion.identity;
        m_fire.Stop();
        m_fire.Clear();
        m_smoke.Stop();
        m_smoke.Clear();
        m_isOnWater = false;
        m_rotationSpeed = 0.0f;
        m_fallingSpeed = 0.0f;
    }

    private void Update()
    {
        var position = transform.position;
        position.y -= m_fallingSpeed * Time.deltaTime;
        transform.position = position;

        var rotation = transform.rotation;
        rotation = rotation * Quaternion.AngleAxis(m_rotationSpeed * Time.deltaTime, Vector3.forward);
        transform.rotation = rotation;

        if (!m_isOnWater)
        {
            var waterStripIndex = m_water.GetWaterStripIndex(transform.position.x);
            var waterHeight = m_water.GetWaterHeight(waterStripIndex);
            if (waterHeight >= transform.position.y)
            {
                m_isOnWater = true;
                m_water.Impulse(waterStripIndex, 4.0f, transform.position.x);
                m_fallingSpeed = 0.1f;
                m_rotationSpeed *= 0.3f;
                SetFireEnabled(false);
            }
        }

        if (transform.position.y < -2.0)
            gameObject.SetActive(false);
    }

    private void StartFalling()
    {
        m_fallingSpeed = Random.Range(0.5f, 0.7f);
        m_rotationSpeed = Random.Range(-120.0f, 120.0f);
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
}

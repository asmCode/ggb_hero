using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour
{
    private ParticleSystem m_particle;
    private WaterStrip m_waterStrip;

    public void Splash(float speed, Vector2 direction, WaterStrip waterStrip)
    {
        m_waterStrip = waterStrip;

        Vector3 position = m_waterStrip.transform.position;
        position.y = m_waterStrip.GetHeight() - 0.1f;
        transform.position = position;

        m_particle.startSpeed = speed;
        m_particle.Play();
    }

    private void Awake()
    {
        m_particle = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        //Vector3 position = m_waterStrip.transform.position;
        //position.y = m_waterStrip.GetHeight() - 1.0f;
        //transform.position = position;
  
        if (!m_particle.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}

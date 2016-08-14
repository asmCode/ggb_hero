using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour
{
    private ParticleSystem m_particle;

    public void Splash(float speed, Vector2 direction, float height, float xCoord)
    {
        Vector3 position = transform.position;
        position.y = height - 0.2f;
        position.x = xCoord;
        transform.position = position;

        m_particle.startSpeed = speed;
        m_particle.Play();
    }

    private void Awake()
    {
        m_particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        //Vector3 position = m_waterStrip.transform.position;
        //position.y = m_waterStrip.GetHeight() - 1.0f;
        //transform.position = position;
  
        if (!m_particle.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
}

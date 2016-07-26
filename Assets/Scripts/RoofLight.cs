using UnityEngine;
using System.Collections;

public class RoofLight : MonoBehaviour
{
    private float m_speed;
    private SpriteRenderer m_sprite;

    private void Awake()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_speed = Random.Range(1.0f, 3.0f);
    }

    private void Update()
    {
        Color color = m_sprite.color;
        color.a = Mathf.Sin(Time.time * m_speed) * 0.5f + 0.5f;
        m_sprite.color = color;
    }
}

using UnityEngine;
using System.Collections;

public class SuperheroControllerFlappy : MonoBehaviour
{
    public Superhero m_superhero;
    public Buttons m_buttons;

    private static readonly Vector2 JumpRightVelocity = new Vector2(4, 4);
    private static readonly Vector2 JumpLeftVelocity = new Vector2(-4, 4);
    private static readonly Vector2 Gravity = new Vector2(0, -10.0f);
    private Vector2 m_velocity;

    void OnEnable()
    {
        m_buttons.LeftButtonPressed += HandleLeftButtonPressed;
        m_buttons.RightButtonPressed += HandleRightButtonPressed;
    }

    void OnDisable()
    {
        m_buttons.LeftButtonPressed += HandleLeftButtonPressed;
        m_buttons.RightButtonPressed += HandleRightButtonPressed;
    }

    private void HandleLeftButtonPressed()
    {
        m_velocity = JumpLeftVelocity;
    }

    private void HandleRightButtonPressed()
    {
        m_velocity = JumpRightVelocity;
    }

    void Update()
    {
        m_velocity += Gravity * Time.deltaTime;

        Vector3 position = m_superhero.transform.position;
        position += new Vector3(m_velocity.x, m_velocity.y, 0) * Time.deltaTime;

        float hmargin = 8.5f;
        float bottom = -4.0f;
        float top = 5.0f;

        if (position.x < -hmargin)
        {
            position.x = -hmargin;
            m_velocity.x = 0;
        }
        if (position.x > hmargin)
        {
            position.x = hmargin;
            m_velocity.x = 0;
        }
        if (position.y > top)
        {
            position.y = top;
            m_velocity.y = 0;
        }
        if (position.y < bottom)
        {
            position.y = bottom;
            m_velocity.y = 0;
        }

        m_superhero.transform.position = position;
    }
}

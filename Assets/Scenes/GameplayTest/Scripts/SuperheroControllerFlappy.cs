using UnityEngine;
using System.Collections;

public class SuperheroControllerFlappy : MonoBehaviour
{
    public Superhero m_superhero;
    public InputControllerProvider m_inputControllerProvider;

    private Buttons m_buttons;

    private static readonly Vector2 JumpRightVelocity = new Vector2(1.5f, 1.5f);
    private static readonly Vector2 JumpLeftVelocity = new Vector2(-1.5f, 1.5f);
    private static readonly Vector2 BaseGravity = new Vector2(0, -4.0f);
    private Vector2 m_velocity;

    private void Awake()
    {
        m_buttons = m_inputControllerProvider.GetButtonsController();
    }

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
        Vector2 gravity = BaseGravity;
        gravity.y -= GameSettings.GravityPerSuicider * m_superhero.GetHoldingSuis();
        m_velocity += gravity * Time.deltaTime;

        Vector3 position = m_superhero.transform.position;
        position += new Vector3(m_velocity.x, m_velocity.y, 0) * Time.deltaTime;
        m_superhero.transform.position = position;
    }
}

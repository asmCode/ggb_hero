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
        m_superhero.Velocity = JumpLeftVelocity;
    }

    private void HandleRightButtonPressed()
    {
        m_superhero.Velocity = JumpRightVelocity;
    }

    void Update()
    {
        Vector2 gravity = BaseGravity;
        gravity.y -= GameSettings.GravityPerSuicider * m_superhero.GetHoldingSuis();
        m_superhero.Velocity = m_superhero.Velocity + gravity * Time.deltaTime;
    }
}

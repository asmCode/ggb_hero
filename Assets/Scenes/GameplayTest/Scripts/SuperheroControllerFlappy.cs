﻿using UnityEngine;
using System.Collections;

public class SuperheroControllerFlappy : MonoBehaviour
{
    public Superhero m_superhero;
    public InputControllerProvider m_inputControllerProvider;

    private static readonly Vector2 JumpRightVelocity = new Vector2(1.5f, 1.5f);
    private static readonly Vector2 JumpLeftVelocity = new Vector2(-1.5f, 1.5f);
    private static readonly Vector2 SwimLeftVelocity = new Vector2(-GameSettings.OnWaterSpeed, 0);
    private static readonly Vector2 SwimRightVelocity = new Vector2(GameSettings.OnWaterSpeed, 0);
    private static readonly Vector2 BaseGravity = new Vector2(0, -3.5f);

    private Buttons m_buttons;
    private bool m_leftPressed;
    private bool m_rightPressed;

    private void Awake()
    {
        m_buttons = m_inputControllerProvider.GetButtonsController();
    }

    void OnEnable()
    {
        m_buttons.LeftButtonPressed += HandleLeftButtonPressed;
        m_buttons.LeftButtonReleased += HandleLeftButtonReleased;
        m_buttons.RightButtonPressed += HandleRightButtonPressed;
        m_buttons.RightButtonReleased += HandleRightButtonReleased;
    }

    void OnDisable()
    {
        m_buttons.LeftButtonPressed -= HandleLeftButtonPressed;
        m_buttons.LeftButtonReleased -= HandleLeftButtonReleased;
        m_buttons.RightButtonPressed -= HandleRightButtonPressed;
        m_buttons.RightButtonReleased -= HandleRightButtonReleased;
    }

    private void HandleLeftButtonPressed()
    {
        m_leftPressed = true;

        if (m_superhero.IsOnWater)
        {
            //m_superhero.Velocity = SwimLeftVelocity;
        }
        else
        {
            m_superhero.Velocity = JumpLeftVelocity;
        }
    }

    private void HandleLeftButtonReleased()
    {
        m_leftPressed = false;

        if (m_superhero.IsOnWater)
        {
            m_superhero.Velocity = JumpLeftVelocity;
        }
    }

    private void HandleRightButtonPressed()
    {
        m_rightPressed = true;

        if (m_superhero.IsOnWater)
        {
            //m_superhero.Velocity = SwimRightVelocity;
        }
        else
        {
            m_superhero.Velocity = JumpRightVelocity;
        }
    }

    private void HandleRightButtonReleased()
    {
        m_rightPressed = false;

        if (m_superhero.IsOnWater)
        {
            m_superhero.Velocity = JumpRightVelocity;
        }
    }

    void Update()
    {
        if (m_superhero.IsOnWater)
        {
            if (m_leftPressed)
                m_superhero.Velocity = SwimLeftVelocity;
            else if (m_rightPressed)
                m_superhero.Velocity = SwimRightVelocity;
        }
        else
        {
            Vector2 gravity = BaseGravity;
            gravity.y -= GameSettings.GravityPerSuicider * m_superhero.GetHoldingSuis();
            m_superhero.Velocity = m_superhero.Velocity + gravity * Time.deltaTime;
        }
    }
}

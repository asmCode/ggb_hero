using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeCinematic : MonoBehaviour
{
    public bool m_fire1Enabled;
    public bool m_fire2Enabled;
    public bool m_fire3Enabled;
    public bool m_cityFireEnabled;
    public bool m_burnMarkEnabled;
    public bool m_fallingBridgeBurstMode;
    public float m_cameraShakePower;

    public event System.Action AnimationFinished;

    public ParticleSystem m_fire1;
    public ParticleSystem m_smoke1;
    public ParticleSystem m_fire2;
    public ParticleSystem m_smoke2;
    public ParticleSystem m_fire3;
    public ParticleSystem m_smoke3;
    public ParticleSystem m_citySmoke;
    public GameObject m_burnMark;
    public FallingBridgeElementGenerator m_fallingBridgeElementGenerator;

    private Animator m_animator;
    private Shaker m_cameraShaker;
    private bool m_animationFinishedCalled;
    private bool m_fallingBridgeBurstModeEnabled = false;

    public void Init(Shaker cameraShaker)
    {
        m_cameraShaker = cameraShaker;
    }

    public void Play()
    {
        m_animationFinishedCalled = false;
        m_animator.Play("EarthQuakeAnimation");
    }

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetParticleEnabled(m_fire1, m_fire1Enabled);
        SetParticleEnabled(m_smoke1, m_fire1Enabled);
        SetParticleEnabled(m_fire2, m_fire2Enabled);
        SetParticleEnabled(m_smoke2, m_fire2Enabled);
        SetParticleEnabled(m_fire3, m_fire3Enabled);
        SetParticleEnabled(m_smoke3, m_fire3Enabled);
        SetParticleEnabled(m_citySmoke, m_cityFireEnabled);
        m_burnMark.SetActive(m_burnMarkEnabled);

        m_cameraShaker.ShakeRange = m_cameraShakePower;

        m_fallingBridgeElementGenerator.gameObject.SetActive(m_fallingBridgeBurstMode);
        if (m_fallingBridgeBurstMode && !m_fallingBridgeBurstModeEnabled)
        {
            m_fallingBridgeBurstModeEnabled = true;
            m_fallingBridgeElementGenerator.SetBurstMode();
        }

        var animState = m_animator.GetCurrentAnimatorStateInfo(0);
        if (animState.IsName("EarthQuakeAnimation") &&
            animState.normalizedTime >= 1 &&
            !m_animationFinishedCalled)
        {
            m_animationFinishedCalled = true;
            if (AnimationFinished != null)
                AnimationFinished();
        }
    }

    private void SetParticleEnabled(ParticleSystem particle, bool enabled)
    {
        if (particle.isPlaying != enabled)
        {
            if (m_fire1Enabled)
            {
                particle.Play();
            }
            else
            {
                particle.Stop();
            }
        }
    }
}

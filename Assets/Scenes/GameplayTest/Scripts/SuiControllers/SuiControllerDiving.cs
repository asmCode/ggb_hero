using UnityEngine;
using System.Collections;

public class SuiControllerDiving : SuiController
{
    private const float SinkingSpeed = 0.08f;
    private const float GhostFlyingSpeed = 0.4f;

    public SuiControllerDiving(Suicider sui) : base(sui)
    {
        AudioManager.GetInstance().SoundDie.Play();
        GameSettings.SuiDeathsCount++;
        sui.DudeAnimator.ClearClip();
    }

    public override void UpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        position.y -= SinkingSpeed * Time.deltaTime;
        m_sui.transform.position = position;
    }

    public override void ProcessTriggerEnter2D(Collider2D other)
    {
        DestroyArea area = other.GetComponent<DestroyArea>();
        if (area == null)
            return;

        m_sui.Destroy();
    }
}

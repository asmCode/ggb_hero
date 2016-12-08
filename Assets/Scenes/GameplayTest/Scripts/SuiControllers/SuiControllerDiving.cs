using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerDiving : SuiController
{
    private const float SinkingSpeed = 0.08f;
    private const float GhostFlyingSpeed = 0.4f;

    public static List<Suicider> Suiciders
    {
        get;
        private set;
    }

    static SuiControllerDiving()
    {
        Suiciders = new List<Suicider>();
    }

    public static void Reset()
    {
        Suiciders.Clear();
    }

    public SuiControllerDiving(Suicider sui) : base(sui)
    {
        Suiciders.Add(sui);

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

    public override void NotifyCollisionWithDestroyArea()
    {
        m_sui.Destroy();
    }

    public override void Leaving()
    {
        if (Suiciders.Count == 0)
        {
            Debug.LogError("Logic error");
            return;
        }

        Suiciders.Remove(m_sui);
    }
}

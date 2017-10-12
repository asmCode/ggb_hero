using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerAlienReturning : SuiController
{
    private float m_animProgress;
    private const float DissapearingTime = 2.0f;

    public static List<Suicider> Suiciders
    {
        get;
        private set;
    }

    static SuiControllerAlienReturning()
    {
        Suiciders = new List<Suicider>();
    }

    public static void Reset()
    {
        Suiciders.Clear();
    }

    public SuiControllerAlienReturning(Suicider sui) : base(sui)
    {
        Suiciders.Add(sui);

        AudioManager.GetInstance().SoundDie.Play();
        GameSettings.SuiDeathsCount++;
        sui.DudeAnimator.ClearClip();

        AlienReturnEffect effect = AlienReturnEffectPool.Instance.Get();
        if (effect != null)
        {
            effect.transform.position = sui.transform.position;
            effect.Play();
        }
    }

    public override void UpdateSui()
    {
        m_animProgress += Time.deltaTime;
        if (m_animProgress >= DissapearingTime)
            m_animProgress = DissapearingTime;

        m_sui.SetOpacity(1.0f - (m_animProgress / DissapearingTime));
    }

    //public override void NotifyCollisionWithDestroyArea()
    //{
    //    m_sui.Destroy();
    //}

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

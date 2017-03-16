using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerFalling : SuiController
{
    private float m_waterHeight;
    private float m_fallingSpeed;

    public static List<Suicider> Suiciders
    {
        get;
        private set;
    }

    public override bool IsGrabable
    {
        get { return true; }
    }

    static SuiControllerFalling()
    {
        Suiciders = new List<Suicider>();
    }

    public static void Reset()
    {
        Suiciders.Clear();
    }

    public SuiControllerFalling(Suicider sui) : base(sui)
    {
        Suiciders.Add(sui);

        m_waterHeight = sui.WaterLevel.GetWaterHeight(sui.transform.position.x, true);

        m_fallingSpeed = Random.Range(GameSettings.SuiFallingSpeedMin, GameSettings.SuiFallingSpeedMax);
        sui.IsKinematic = true;
        sui.Dude.SetBobyPartsKinematic(true);
        sui.DudeAnimator.Fall();
    }

    public override void UpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        position.y -= m_fallingSpeed * Time.deltaTime;
        m_sui.transform.position = position;
    }

    public override void LateUpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        if (position.y <= m_waterHeight)
        {
            m_sui.SetController(new SuiControllerSinking(m_sui));

            WaterSplash splash = WaterSplashPool.Instance.Get();
            if (splash != null)
            {
                splash.Splash(1.0f, Vector2.up, m_waterHeight, position.x);
            }
            AudioManager.GetInstance().SoundWaterSplash.Play();
        }
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

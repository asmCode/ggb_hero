using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerSinking : SuiController
{
    private float m_waterHeight;
    private const float SinkinkTime = 4.0f;
    private float m_time;

    public static List<Suicider> Suiciders
    {
        get;
        private set;
    }

    public override bool IsGrabable
    {
        get { return true; }
    }

    static SuiControllerSinking()
    {
        Suiciders = new List<Suicider>();
    }

    public static void Reset()
    {
        Suiciders.Clear();
    }

    public SuiControllerSinking(Suicider sui) : base(sui)
    {
        Suiciders.Add(sui);

        m_waterHeight = sui.WaterLevel.GetWaterHeight(sui.transform.position.x, true);

        sui.IsKinematic = true;
        sui.Dude.SetBobyPartsKinematic(true);
        sui.DudeAnimator.Sink();
        m_sui.SetHealthBarVisible(true);
        m_sui.SetHealthValue(1.0f);

        if (m_sui.WaterCircles != null)
        {
            m_sui.WaterCircles.Stop();
            m_sui.WaterCircles = null;
        }

        m_sui.WaterCircles = WaterCirclesPool.Instance.Get();
        if (m_sui.WaterCircles != null)
            m_sui.WaterCircles.Play(m_sui.transform.position);

        m_sui.m_waterWaiver.Reset();
    }

    public override void UpdateSui()
    {
        m_time += Time.deltaTime;

        m_sui.SetHealthValue(1.0f - m_time / SinkinkTime);

        GameSettings.PenaltyTime += Time.deltaTime;

        if (m_time < SinkinkTime)
            return;

        m_sui.SetController(new SuiControllerDiving(m_sui));
    }

    public override void LateUpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        position.y = m_waterHeight;
        position.y += m_sui.m_waterWaiver.GetValue(Time.deltaTime);
        m_sui.transform.position = position;
    }

    public override void Leaving()
    {
        if (m_sui.WaterCircles != null)
        {
            m_sui.WaterCircles.Stop();
            m_sui.WaterCircles = null;
        }

        if (Suiciders.Count == 0)
        {
            Debug.LogError("Logic error");
            return;
        }

        m_sui.SetHealthBarVisible(false);
        Suiciders.Remove(m_sui);
    }
}

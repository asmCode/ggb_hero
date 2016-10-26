using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerSinking : SuiController
{
    private const float SinkinkTime = 4.0f;
    private float m_time;
    private int m_waterStripIndex;

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

        m_waterStripIndex = sui.Water.GetWaterStripIndex(sui.transform.position.x);

        sui.IsKinematic = true;
        sui.Dude.SetBobyPartsKinematic(true);
        sui.DudeAnimator.Sink();
        m_sui.SetHealthBarVisible(true);
        m_sui.SetHealthValue(1.0f);
    }

    public override void UpdateSui()
    {
        m_time += Time.deltaTime;

        m_sui.SetHealthValue(1.0f - m_time / SinkinkTime);

        if (m_time < SinkinkTime)
            return;

        m_sui.SetController(new SuiControllerDiving(m_sui));
    }

    public override void LateUpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        position.y = m_sui.Water.GetWaterHeight(m_waterStripIndex);
        m_sui.transform.position = position;
    }

    public override void Leaving()
    {
        if (Suiciders.Count == 0)
        {
            Debug.LogError("Logic error");
            return;
        }

        m_sui.SetHealthBarVisible(false);
        Suiciders.Remove(m_sui);
    }
}

using UnityEngine;
using System.Collections;

public class SuiControllerSinking : SuiController
{
    private const float SinkinkTime = 5.0f;
    private float m_time;

    public override bool IsGrabable
    {
        get { return true; }
    }

    public SuiControllerSinking(Suicider sui) : base(sui)
    {
    }

    public override void UpdateSui()
    {
        m_time += Time.deltaTime;

        if (m_time < SinkinkTime)
            return;

        m_sui.SetController(new SuiControllerDiving(m_sui));
    }
}

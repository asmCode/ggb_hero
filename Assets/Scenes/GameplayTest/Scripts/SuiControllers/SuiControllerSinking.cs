using UnityEngine;
using System.Collections;

public class SuiControllerSinking : SuiController
{
    private const float SinkinkTime = 3.0f;
    private float m_time;
    private int m_waterStripIndex;

    public override bool IsGrabable
    {
        get { return true; }
    }

    public SuiControllerSinking(Suicider sui) : base(sui)
    {
        m_waterStripIndex = sui.Water.GetWaterStripIndex(sui.transform.position.x);
    }

    public override void UpdateSui()
    {
        m_time += Time.deltaTime;

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
}

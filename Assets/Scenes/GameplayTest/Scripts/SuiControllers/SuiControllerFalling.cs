using UnityEngine;
using System.Collections;

public class SuiControllerFalling : SuiController
{
    private static readonly float FallingSpeed = 0.25f;
    private int m_waterStripIndex;

    public override bool IsGrabable
    {
        get { return true; }
    }

    public SuiControllerFalling(Suicider sui) : base(sui)
    {
        m_waterStripIndex = sui.Water.GetWaterStripIndex(sui.transform.position.x);
    }

    public override void UpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        position.y -= FallingSpeed * Time.deltaTime;
        m_sui.transform.position = position;
    }

    public override void LateUpdateSui()
    {
        if (m_sui.transform.position.y <= m_sui.Water.GetWaterHeight(m_waterStripIndex))
        {
            m_sui.SetController(new SuiControllerSinking(m_sui));
            m_sui.Water.Impulse(m_waterStripIndex, FallingSpeed * 3.0f);
        }
    }
}

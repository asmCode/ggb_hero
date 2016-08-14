using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerFalling : SuiController
{
    private static readonly float BaseFallingSpeed = 0.25f;
    private int m_waterStripIndex;
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

        m_waterStripIndex = sui.Water.GetWaterStripIndex(sui.transform.position.x);

        m_fallingSpeed = BaseFallingSpeed * Random.Range(0.8f, 1.4f);
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
        if (position.y <= m_sui.Water.GetWaterHeight(m_waterStripIndex))
        {
            m_sui.SetController(new SuiControllerSinking(m_sui));
            m_sui.Water.Impulse(m_waterStripIndex, m_fallingSpeed * 8.0f, position.x);
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

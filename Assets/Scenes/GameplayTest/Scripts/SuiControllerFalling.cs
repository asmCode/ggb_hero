using UnityEngine;
using System.Collections;

public class SuiControllerFalling : SuiController
{
    private static readonly float FallingSpeed = 0.25f;

    public override bool IsGrabable
    {
        get { return true; }
    }

    public SuiControllerFalling(Suicider sui) : base(sui)
    {
    }

    public override void UpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        position.y -= FallingSpeed * Time.deltaTime;
        m_sui.transform.position = position;
    }

    public override void ProcessTriggerEnter(Collider other)
    {
        DeathArea deathArea = other.GetComponent<DeathArea>();
        if (deathArea == null)
            return;

        m_sui.SetController(new SuiControllerSinking(m_sui));
    }
}

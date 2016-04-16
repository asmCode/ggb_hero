using UnityEngine;
using System.Collections;

public class SuiControllerDiving : SuiController
{
    private const float SinkingSpeed = 0.08f;
    private const float GhostFlyingSpeed = 0.4f;

    public SuiControllerDiving(Suicider sui) : base(sui)
    {
        GameSettings.SuiDeathsCount++;
    }

    public override void UpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        position.y -= SinkingSpeed * Time.deltaTime;
        m_sui.transform.position = position;
    }

    public override void ProcessTriggerEnter(Collider other)
    {
        DestroyArea area = other.GetComponent<DestroyArea>();
        if (area == null)
            return;

        Object.Destroy(m_sui.gameObject);
    }
}

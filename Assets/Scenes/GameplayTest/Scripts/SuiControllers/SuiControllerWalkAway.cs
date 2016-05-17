using UnityEngine;
using System.Collections;

public class SuiControllerWalkAway : SuiController
{
    private Walker m_walker;

    public SuiControllerWalkAway(Suicider sui) : base(sui)
    {
        float direction = m_sui.transform.position.x < 0.0f ? -1.0f : 1.0f;
        m_walker = new Walker(sui.transform, direction, 0.3f);
    }

    public override void UpdateSui()
    {
        m_walker.Update();
    }

    public override void ProcessTriggerEnter2D(Collider2D other)
    {
        DestroyArea area = other.GetComponent<DestroyArea>();
        if (area == null)
            return;

        Object.Destroy(m_sui.transform.parent.gameObject);
    }
}

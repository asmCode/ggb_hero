using UnityEngine;
using System.Collections;

public class SuiController
{
    protected Suicider m_sui;

    public SuiController(Suicider sui)
    {
        m_sui = sui;
    }

    public virtual void UpdateSui()
    {

    }

    public virtual void ProcessTriggerEnter(Collider other)
    {
        DeathArea deathArea = other.GetComponent<DeathArea>();
        if (deathArea == null)
            return;

        Object.Destroy(m_sui.gameObject);

        GameSettings.SuiDeathsCount++;
    }
}

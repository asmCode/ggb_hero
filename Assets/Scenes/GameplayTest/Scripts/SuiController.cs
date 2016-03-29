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
}

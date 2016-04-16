﻿using UnityEngine;
using System.Collections;

public class SuiController
{
    protected Suicider m_sui;

    public virtual bool IsGrabable
    {
        get { return false; }
    }

    public SuiController(Suicider sui)
    {
        m_sui = sui;
    }

    public virtual void UpdateSui()
    {

    }

    public virtual void ProcessTriggerEnter(Collider other)
    {
    }
}
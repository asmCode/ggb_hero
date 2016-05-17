using UnityEngine;
using System.Collections;

public class SuiControllerIdleTest : SuiController
{
    public override bool IsGrabable
    {
        get { return true; }
    }

    public SuiControllerIdleTest(Suicider sui) : base(sui)
    {
    }
}

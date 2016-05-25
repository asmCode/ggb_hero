using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerWithSuperhero : SuiController
{
    public static List<Suicider> Suiciders
    {
        get;
        private set;
    }

    static SuiControllerWithSuperhero()
    {
        Suiciders = new List<Suicider>();
    }

    public static void Reset()
    {
        Suiciders.Clear();
    }

    public SuiControllerWithSuperhero(Suicider sui) : base(sui)
    {
        Suiciders.Add(sui);
    }

    public override void UpdateSui()
    {
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

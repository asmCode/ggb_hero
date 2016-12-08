using UnityEngine;
using System.Collections.Generic;

public class SuiControllerWalkAway : SuiController
{
    private Walker m_walker;

    public static List<Suicider> Suiciders
    {
        get;
        private set;
    }

    static SuiControllerWalkAway()
    {
        Suiciders = new List<Suicider>();
    }

    public static void Reset()
    {
        Suiciders.Clear();
    }

    public SuiControllerWalkAway(Suicider sui) : base(sui)
    {
        Suiciders.Add(sui);

        float direction = m_sui.transform.position.x < 0.0f ? -1.0f : 1.0f;
        m_walker = new Walker(sui.transform, direction, Random.Range(0.2f, 0.4f));
        sui.IsKinematic = true;
        sui.Dude.SetBobyPartsKinematic(true);
        sui.DudeAnimator.Walk();
        sui.Reset();
    }

    public override void UpdateSui()
    {
        m_walker.Update();
    }

    public override void NotifyCollisionWithDestroyArea()
    {
        m_sui.Destroy();
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

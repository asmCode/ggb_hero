using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiControllerWalkOnBridge : SuiController
{
    public static List<Suicider> Suiciders
    {
        get;
        private set;
    }

    private float m_horiDirection;
    private Walker m_walker;
    private RectBounds m_bridgeWalkArea;

    static SuiControllerWalkOnBridge()
    {
        Suiciders = new List<Suicider>();
    }

    public static void Reset()
    {
        Suiciders.Clear();
    }

    public SuiControllerWalkOnBridge(Suicider sui, Vector2 startPosition, float direction, RectBounds bridgeWalkArea) : base(sui)
    {
        Suiciders.Add(sui);

        sui.transform.position = startPosition;
        m_horiDirection = direction;
        float speed = Random.Range(0.2f, 0.4f);
        m_walker = new Walker(sui.transform, m_horiDirection, speed);
        m_bridgeWalkArea = bridgeWalkArea;
        sui.Dude.SetBobyPartsKinematic(true);
        sui.DudeAnimator.Walk();
    }

    public override void UpdateSui()
    {
        m_walker.Update();

        if (!m_bridgeWalkArea.IsCoordInsideHori(m_sui.transform.position.x))
        {
            m_sui.Destroy();
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

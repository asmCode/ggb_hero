using UnityEngine;
using System.Collections;

public class SuiControllerWalkOnBridge : SuiController
{
    private float m_horiDirection;
    private Walker m_walker;
    private RectBounds m_bridgeWalkArea;

    public SuiControllerWalkOnBridge(Suicider sui, Vector2 startPosition, float direction, RectBounds bridgeWalkArea) : base(sui)
    {
        sui.transform.position = startPosition;
        m_horiDirection = direction;
        float speed = Random.Range(0.2f, 0.4f);
        m_walker = new Walker(sui.transform, m_horiDirection, speed);
        m_bridgeWalkArea = bridgeWalkArea;
    }

    public override void UpdateSui()
    {
        m_walker.Update();

        if (!m_bridgeWalkArea.IsCoordInsideHori(m_sui.transform.position.x))
        {
            Object.Destroy(m_sui.transform.parent.gameObject);
        }
    }
}

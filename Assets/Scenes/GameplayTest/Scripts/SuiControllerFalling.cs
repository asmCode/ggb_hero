using UnityEngine;
using System.Collections;

public class SuiControllerFalling : SuiController
{
    private static readonly float FallingSpeed = 0.25f;

    public SuiControllerFalling(Suicider sui, Vector2 startPosition) : base(sui)
    {
        sui.transform.position = startPosition;
    }

    public override void UpdateSui()
    {
        Vector3 position = m_sui.transform.position;
        position.y -= FallingSpeed * Time.deltaTime;
        m_sui.transform.position = position;
    }
}

using UnityEngine;
using System.Collections;

public class SuiControllerRescuing : SuiController
{
    private Vector2 m_basePosition;
    private Vector2 m_destinationPosition;
    private Vector2 m_velocity;

    public SuiControllerRescuing(Suicider sui, Vector2 destinationPosition) : base(sui)
    {
        m_basePosition = sui.transform.position;
        m_destinationPosition = destinationPosition;
    }
	
	public override void UpdateSui()
    {
        m_sui.transform.position =
            Vector2.SmoothDamp(m_sui.transform.position, m_destinationPosition, ref m_velocity, 0.2f);
	}
}

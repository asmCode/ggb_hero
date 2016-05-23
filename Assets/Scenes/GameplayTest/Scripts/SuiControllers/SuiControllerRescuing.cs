using UnityEngine;
using System.Collections;

public class SuiControllerRescuing : SuiController
{
    private Vector2 m_destinationPosition;
    private Vector2 m_velocity;

    public SuiControllerRescuing(Suicider sui, Vector2 destinationPosition) : base(sui)
    {
        m_destinationPosition = destinationPosition;
        sui.IsKinematic = true;
    }
	
	public override void UpdateSui()
    {
        m_sui.transform.position =
            Vector2.SmoothDamp(m_sui.transform.position, m_destinationPosition, ref m_velocity, 0.2f);

        Vector2 suiPosition = m_sui.transform.position;

        if ((m_destinationPosition - suiPosition).magnitude < 0.01f)
        {
            m_sui.SetController(new SuiControllerWalkAway(m_sui));
        }
	}
}

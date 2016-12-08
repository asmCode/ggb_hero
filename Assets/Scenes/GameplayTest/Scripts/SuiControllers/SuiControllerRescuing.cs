using UnityEngine;
using System.Collections;

public class SuiControllerRescuing : SuiController
{
    private Vector2 m_destinationPosition;
    private Vector2 m_velocity;

    public SuiControllerRescuing(Suicider sui, Vector2 destinationPosition) : base(sui)
    {
        m_destinationPosition = destinationPosition + Random.insideUnitCircle * 0.06f;
        sui.IsKinematic = true;
        sui.Dude.SetBobyPartsKinematic(true);
        // This also looks cool
        // sui.DudeAnimator.Walk();
        sui.DudeAnimator.SetupPivots();
        sui.Reset();
    }
	
	public override void UpdateSui()
    {
        Vector2 newPosition =
            Vector2.SmoothDamp(m_sui.transform.position, m_destinationPosition, ref m_velocity, 0.13f, Mathf.Infinity, Time.deltaTime);

        m_sui.transform.position = new Vector3(newPosition.x, newPosition.y, m_sui.transform.position.z);

        Vector2 suiPosition = m_sui.transform.position;

        if ((m_destinationPosition - suiPosition).magnitude < 0.05f)
        {
            m_sui.SetController(new SuiControllerWalkAway(m_sui));
        }
	}
}

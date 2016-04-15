using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    private SuiController m_suiController;

    public bool IsGrabable
    {
        get
        {
            return m_suiController.IsGrabable;
        }
    }

    public void SetController(SuiController suiController)
    {
        m_suiController = suiController;
    }

    void Update()
    {
        m_suiController.UpdateSui();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_suiController.ProcessTriggerEnter(other);
    }
}

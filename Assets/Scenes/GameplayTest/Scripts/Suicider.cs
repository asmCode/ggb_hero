using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    private SuiController m_suiController;

    public void SetController(SuiController suiController)
    {
        m_suiController = suiController;
    }

    void Update()
    {
        m_suiController.UpdateSui();
    }
}

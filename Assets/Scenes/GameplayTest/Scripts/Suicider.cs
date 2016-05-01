using UnityEngine;
using System.Collections;

public class Suicider : MonoBehaviour
{
    private SuiController m_suiController;

    public Water Water
    {
        get;
        private set;
    }

    public bool IsGrabable
    {
        get
        {
            return m_suiController.IsGrabable;
        }
    }

    public void Initialize(Water water)
    {
        Water = water;
    }

    public void SetController(SuiController suiController)
    {
        m_suiController = suiController;
    }

    void Update()
    {
        m_suiController.UpdateSui();
    }

    void LateUpdate()
    {
        m_suiController.LateUpdateSui();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_suiController.ProcessTriggerEnter(other);
    }
}

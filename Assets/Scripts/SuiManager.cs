using UnityEngine;
using System.Collections;

public class SuiManager : MonoBehaviour
{
    public SuiciderGenerator m_suiGenerator;
    public Transform m_suiContainer;

    public void Reset()
    {
        m_suiGenerator.gameObject.SetActive(false);
        while (m_suiContainer.childCount > 0)
        {
            Transform child = m_suiContainer.GetChild(0);
            child.parent = null;
            Destroy(child.gameObject);
        }
    }
}

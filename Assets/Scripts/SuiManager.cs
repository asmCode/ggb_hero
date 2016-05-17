using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiManager : MonoBehaviour
{
    public SuiciderGenerator m_suiGenerator;

    public void Reset()
    {
        m_suiGenerator.gameObject.SetActive(false);
        m_suiGenerator.Reset();
    }
}

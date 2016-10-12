using UnityEngine;
using System.Collections;

public class HudView : MonoBehaviour
{
    public UILabel m_rescuedValue;
    public UILabel m_deathsValue;

    void Update()
    {
        m_rescuedValue.text = GameSettings.SuiRescuedCount.ToString();
        m_deathsValue.text = string.Format("{0}/{1}", GameSettings.SuiDeathsCount, GameSettings.SuiDeathsLimit);
    }
}

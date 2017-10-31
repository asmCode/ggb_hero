using UnityEngine;
using System.Collections;

public class HudView : MonoBehaviour
{
    public UILabel m_rescuedValue;
    public UILabel m_deathsValue;
    public UIProgressBar m_penaltyMeter;

    void Update()
    {
        m_rescuedValue.text = GameSettings.SuiRescuedCount.ToString();
        m_deathsValue.text = string.Format("{0}/{1}", GameSettings.SuiDeathsCount, GameSettings.SuiDeathsLimit);
        m_penaltyMeter.value = Mathf.Min(1f, GameSettings.PenaltyTime / GameSettings.PenaltyTimeLimit);
    }
}

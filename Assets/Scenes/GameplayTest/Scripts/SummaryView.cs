using UnityEngine;
using Ssg.Ads;

public class SummaryView : MonoBehaviour
{
    public UILabel m_rescuedValue;
    public UILabel m_recordValue;
    public GameObject m_continueButton;

    public event System.Action ContinueClicked;
    public event System.Action PlayAgainClicked;

    public void Show()
    {
        m_rescuedValue.text = GameSettings.SuiRescuedCount.ToString();
        int record = PlayerPrefs.GetInt("record", 0);
        if (record < GameSettings.SuiRescuedCount)
        {
            record = GameSettings.SuiRescuedCount;
            PlayerPrefs.SetInt("record", record);
        }
        m_recordValue.text = record.ToString();

        NGUITools.SetActive(m_continueButton.gameObject, RewardedAds.GetInstance().IsReady());
    }

    public void HandleContinueClicked()
    {
        if (ContinueClicked != null)
        {
            ContinueClicked();
        }
    }

    public void HandleRestartClicked()
    {
        if (PlayAgainClicked != null)
        {
            PlayAgainClicked();
        }
    }
}

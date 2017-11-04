using UnityEngine;
using Ssg.Ads;

public class SummaryView : MonoBehaviour
{
    public UILabel m_labelSuicides;
    public UILabel m_rescuedValue;
    public UILabel m_recordValue;
    public GameObject m_continueButton;
    public Gameplay m_gameplay;

    public event System.Action ContinueClicked;
    public event System.Action PlayAgainClicked;

    public void Show()
    {
        m_labelSuicides.gameObject.SetActive(!GameSettings.Censore);

        m_rescuedValue.text = GameSettings.SuiRescuedCount.ToString();
        GGHeroGame.SaveScore(m_gameplay, GameSettings.SuiRescuedCount);

        m_recordValue.text = GGHeroGame.GetRecord().ToString();

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

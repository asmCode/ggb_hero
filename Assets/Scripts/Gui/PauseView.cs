using UnityEngine;
    
public class PauseView : MonoBehaviour
{
    public UILabel m_soundLabel;

    public Gameplay Gameplay { get; set; }

    private void OnEnable()
    {
        UpdateButtons();
    }

    public void ResumeButtonPressed()
    {
        NGUITools.SetActive(gameObject, false);
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ResumeButtonPressed();
    }

    public void SoundsButtonPressed()
    {
        AudioManager.GetInstance().SetSoundsEnabled(!AudioManager.GetInstance().SoundsEnabled);
        UpdateButtons();
    }

    public void PlayAgainButtonPressed()
    {
        Gameplay.RestartGame(false);
    }

    public void ExitToMenuButtonPressed()
    {
        GGHeroGame.SaveScore(Gameplay, GameSettings.SuiRescuedCount);
        Gameplay.RestartGame(true);
    }

    private void UpdateButtons()
    {
        m_soundLabel.text = AudioManager.GetInstance().SoundsEnabled ? "SOUNDS ON" : "SOUNDS OFF";
    }
}

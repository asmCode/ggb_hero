using UnityEngine;
    
public class PauseView : MonoBehaviour
{
    public UILabel m_soundLabel;
    public UILabel m_musicLabel;

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

    public void SoundsButtonPressed()
    {
        AudioManager.GetInstance().SetSoundsEnabled(!AudioManager.GetInstance().SoundsEnabled);
        UpdateButtons();
    }

    public void MusicBeuttonPressed()
    {
        AudioManager.GetInstance().SetMusicEnabled(!AudioManager.GetInstance().MusicEnabled);
        UpdateButtons();
    }

    public void PlayAgainButtonPressed()
    {
        Gameplay.RestartGame(false);
    }

    public void ExitToMenuButtonPressed()
    {
        Gameplay.RestartGame(true);
    }

    private void UpdateButtons()
    {
        m_soundLabel.text = AudioManager.GetInstance().SoundsEnabled ? "SOUNDS ON" : "SOUNDS OFF";
        m_musicLabel.text = AudioManager.GetInstance().MusicEnabled ? "MUSIC ON" : "MUSIC OFF";
    }
}

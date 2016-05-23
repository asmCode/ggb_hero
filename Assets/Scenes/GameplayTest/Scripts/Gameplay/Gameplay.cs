using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    public Superhero m_superhero;
    public SuperheroControllerFlappy m_superheroController;
    public SuiManager m_suiManager;
    public SummaryView m_summaryView;

    private float start_time = 0.0f;
    private bool m_isRoundEnded = false;

    private float GameplayTime
    {
        get { return Time.time - start_time; }
    }

    void Awake()
    {
        NGUITools.SetActive(m_summaryView.gameObject, false);
    }

    void Start()
    {
        start_time = Time.time;
        m_suiManager.m_suiGenerator.SuicidersDelay = GameSettings.SuiJumpDelayEasiest;

    }

    void Update()
    {
        m_suiManager.m_suiGenerator.SuicidersDelay = CalculateSuiDelay();

        if (!m_isRoundEnded && GameSettings.SuiDeathsCount == GameSettings.SuiDeathsLimit)
        {
            EndRound();
        }
    }

    void EndRound()
    {
        DestroySuperhero();
        DestroySuiciders();

        NGUITools.SetActive(m_summaryView.gameObject, true);
        m_summaryView.Show();

        m_isRoundEnded = true;
    }

    void DestroySuperhero()
    {
        Destroy(m_superhero.gameObject);
        Destroy(m_superheroController.gameObject);
    }

    void DestroySuiciders()
    {
        m_suiManager.Reset();
    }

    private float CalculateSuiDelay()
    {
        float normalized_time = Mathf.Clamp01(GameplayTime / GameSettings.SuiJumpDelayHardestAfterTime);
        return Mathf.Lerp(GameSettings.SuiJumpDelayEasiest, GameSettings.SuiJumpDelayHardest, normalized_time);
    }
}

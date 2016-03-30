using UnityEngine;
using System.Collections;

public class GameplayTestController : MonoBehaviour
{
    public Superhero m_superhero;
    public SuperheroControllerFlappy m_superheroController;
    public SuiManager m_suiManager;
    public SummaryView m_summaryView;

    private bool m_isRoundEnded = false;

    void Awake()
    {
        NGUITools.SetActive(m_summaryView.gameObject, false);
    }

    void Update()
    {
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
}

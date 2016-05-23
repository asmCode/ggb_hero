using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    public Superhero m_superhero;
    public SuperheroControllerFlappy m_superheroController;
    public SuiManager m_suiManager;
    public SummaryView m_summaryView;
    public GrabPanel m_grabPanel;

    private float start_time = 0.0f;
    private bool m_isRoundEnded = false;

    //private int[] m_incGrabGoals = { 10, 30, 60, 100 };
    private int[] m_incGrabGoals = { 5, 15, 30, 60 };
    //private int m_incGrabCount = 0;
    //private int m_grabCount = 1;

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

        UpdateGrabStats();

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

    private void UpdateGrabStats()
    {
        int grabCapacity;
        int suiSaved;
        int suiGoal;
        CalcGrabStats(GameSettings.SuiRescuedCount, out grabCapacity, out suiSaved, out suiGoal);

        m_grabPanel.SetGrabCapacity(grabCapacity);
        GameSettings.HandCapacity = grabCapacity;

        if (suiGoal > 0)
        {
            m_grabPanel.SetGrabCounter(suiSaved, suiGoal);
        }
        else
        {
            m_grabPanel.ShowGrabCounter(false);
        }

        //for (int i = 0; i < 100; i++)
        //{
        //    CalcGrabStats(i, out grabCapacity, out suiSaved, out suiGoal);
        //    Debug.LogFormat("{0}: cap: {1}, {2}/{3}", i, grabCapacity, suiSaved, suiGoal);
        //}
    }

    private void CalcGrabStats(int totalSuiRescued, out int grabCapacity, out int suiSaved, out int suiGoal)
    {
        int index = 0;
        for (index = 0; index < m_incGrabGoals.Length; index++)
        {
            if (totalSuiRescued < m_incGrabGoals[index])
                break;
            totalSuiRescued -= m_incGrabGoals[index];
        }

        if (index < m_incGrabGoals.Length)
        {
            grabCapacity = index + 1;
            suiSaved = totalSuiRescued;
            suiGoal = m_incGrabGoals[index];
        }
        else
        {
            grabCapacity = m_incGrabGoals.Length + 1;
            suiSaved = 0;
            suiGoal = 0;
        }
    }
}

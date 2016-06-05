﻿using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    public Superhero m_superhero;
    public SuperheroControllerFlappy m_superheroController;
    public SuiManager m_suiManager;
    public SummaryView m_summaryView;
    public GrabPanel m_grabPanel;
    public WaveIndicator m_waveIndicator;

    private float start_time = 0.0f;
    private bool m_isRoundEnded = false;

    //private int[] m_incGrabGoals = { 10, 30, 60, 100 };
    private int[] m_incGrabGoals = { 5, 15, 30, 60 };
    //private int m_incGrabCount = 0;
    //private int m_grabCount = 1;

    private int m_waveNumber = 0;
    private float m_currentWaveLength;
    private float m_currentWaveTime;
    private bool m_waitingForNextWave;

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

        //for (int i = 0; i < 15; i++)
        //{
        //    Debug.LogFormat("=== WAVE {0}: Time {1}", i, GetWaveLength(i));

        //    for (float t = 0.0f; t < GetWaveLength(i); t += 10.0f)
        //    {
        //        CalculateSuiDelay(i, t);
        //    }
        //}

        m_currentWaveLength = GetWaveLength(m_waveNumber);
        m_waitingForNextWave = true;
        ShowCurrentWaveTitle();
        Invoke("StartWave", 2.0f);
    }

    void NextWave()
    {
        GameSettings.SuiDeathsCount = 0;

        m_waveNumber++;
        m_currentWaveLength = GetWaveLength(m_waveNumber);
        m_currentWaveTime = 0.0f;

        ShowCurrentWaveTitle();
        Invoke("StartWave", 2.0f);
    }

    private void ShowCurrentWaveTitle()
    {
        m_waveIndicator.ShowWave(m_waveNumber + 1);
    }

    private void StartWave()
    {
        m_suiManager.m_suiGenerator.JumpSuis = true;
        m_waitingForNextWave = false;
    }

    void Update()
    {
        if (m_isRoundEnded)
            return;

        m_suiManager.m_suiGenerator.SuicidersDelay = CalculateSuiDelay(m_waveNumber, m_currentWaveTime);

        m_currentWaveTime += Time.deltaTime;
        if (m_currentWaveTime >= m_currentWaveLength)
        {
            m_suiManager.m_suiGenerator.JumpSuis = false;

            if (!m_waitingForNextWave)
            {
                if (SuiControllerPreparingForJump.Suiciders.Count == 0 &&
                    SuiControllerFalling.Suiciders.Count == 0 &&
                    SuiControllerSinking.Suiciders.Count == 0 &&
                    SuiControllerWithSuperhero.Suiciders.Count == 0)
                {
                    m_waitingForNextWave = true;
                    NextWave();
                }
            }
        }

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

        NGUITools.SetActive(m_waveIndicator.gameObject, false);
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

        SuiControllerFalling.Reset();
        SuiControllerPreparingForJump.Reset();
        SuiControllerWalkOnBridge.Reset();
        SuiControllerSinking.Reset();
        SuiControllerFalling.Reset();
        SuiControllerWithSuperhero.Reset();
    }

    private float CalculateSuiDelay(int waveNumber, float waveTime)
    {
        /*
        // Endless mode
        float normalized_time = Mathf.Clamp01(GameplayTime / GameSettings.SuiJumpDelayHardestAfterTime);
        return Mathf.Lerp(GameSettings.SuiJumpDelayEasiest, GameSettings.SuiJumpDelayHardest, normalized_time);
        */

        // Waves mode

        float normalizedWaveNumber = Mathf.Clamp01((float)waveNumber / GameSettings.LastWave);
        float waveLength = Mathf.Lerp(GameSettings.WaveLengthFirstWave, GameSettings.WaveLengthLastWave, normalizedWaveNumber);
        float normalizedTime = Mathf.Clamp01(waveTime / waveLength);
        float suiDelayEasy = Mathf.Lerp(GameSettings.SuiJumpDelayEasiestFirstWave, GameSettings.SuiJumpDelayEasiestLastWave, normalizedWaveNumber);
        float suiDelayHard = Mathf.Lerp(GameSettings.SuiJumpDelayHardestFirstWave, GameSettings.SuiJumpDelayHardestLastWave, normalizedWaveNumber);
        float suiDelay = Mathf.Lerp(suiDelayEasy, suiDelayHard, normalizedTime);

        //Debug.LogFormat("number: {0}, time: {1}, suiDelay = {2}", waveNumber, waveTime, suiDelay);

        return suiDelay;
    }

    private float GetWaveLength(int waveNumber)
    {
        float normalizedWaveNumber = Mathf.Clamp01((float)waveNumber / GameSettings.LastWave);
        float waveLength = Mathf.Lerp(GameSettings.WaveLengthFirstWave, GameSettings.WaveLengthLastWave, normalizedWaveNumber);
        return waveLength;
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

        GameSettings.HandCapacity = 3;
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
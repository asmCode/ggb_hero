using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
using Ssg.Ads;

public class Gameplay : MonoBehaviour
{
    static bool m_playAgain;
    public GameObject m_pauseButton;
    public Material m_bridgeMaterial;
    public Superhero m_superhero;
    public SuperheroControllerFlappy m_superheroController;
    public SuiManager m_suiManager;
    public SummaryView m_summaryView;
    public PauseView m_pauseView;
    public GrabPanel m_grabPanel;
    public WaveIndicator m_waveIndicator;
    public Transform m_shoreArrows;
    public GameObject m_beforeStartFade;
    public GameObject m_beforeStartTutorial;
    public GameObject m_swimmingTutorial;
    public GameObject m_startScreen;
    public GameObject m_hud;
    public RectBounds[] m_suiDestroyAreas;
    public UILabel m_record;
    public UILabel m_labelRankValue;
    public UILabel m_labelRankText;
    public UILabel m_labelSummaryRankValue;
    public UILabel m_labelSummaryRankText;
    public UILabel m_version;
    public UISprite m_speakerIcon;
    public FallingBridgeElementGenerator m_fallingBridgeGemerator;
    public GameObject m_fps;
    public Transform m_cameraShakeRoot;
    public EarthQuakeCinematic m_earthQuakeCinematic;

    internal GameplayState m_state;
    internal float start_time = 0.0f;
    internal bool m_isRoundEnded = false;

    private bool m_shakeCamera = false;
    private Shaker m_cameraShaker = new Shaker(0.0f, 15.0f);

    //private int[] m_incGrabGoals = { 10, 30, 60, 100 };
    private int[] m_incGrabGoals = { 5, 15, 30, 60 };
    //private int m_incGrabCount = 0;
    //private int m_grabCount = 1;

    internal int m_waveNumber = 0;
    internal float m_currentWaveLength;
    internal float m_currentWaveTime;
    internal bool m_waitingForNextWave;

    private long m_cachedLeaderboardRank = 0;

    private float GameplayTime
    {
        get { return Time.time - start_time; }
    }

    public void SetRecord(int record)
    {
        m_record.text = record.ToString();
    }

    public void ChangeState(GameplayState state)
    {
        if (m_state != null)
        {
            m_state.Leave();
        }

        m_state = state;

        if (m_state != null)
        {
            m_state.Enter();
        }
    }

    public void SetStartScreen()
    {
        m_superheroController.gameObject.SetActive(false);
        SetTutorialVisible(false);
        SetHudVisible(false);
        m_swimmingTutorial.gameObject.SetActive(false);

        ChangeState(new GameplayStateStart(this));
    }

    public void SetStartScreenVisible(bool visible)
    {
        NGUITools.SetActive(m_startScreen, visible);
    }

    public void SetTutorialVisible(bool visible)
    {
        m_beforeStartFade.SetActive(visible);
        m_beforeStartTutorial.SetActive(visible);
    }

    public void SetHudVisible(bool visible)
    {
        NGUITools.SetActive(m_hud.gameObject, visible);
    }

    public void UpdatePlayerRank()
    {
        if (m_cachedLeaderboardRank == 0)
        {
            NGUITools.SetActive(m_labelRankText.gameObject, false);
            NGUITools.SetActive(m_labelRankValue.gameObject, false);
            NGUITools.SetActive(m_labelSummaryRankText.gameObject, false);
            NGUITools.SetActive(m_labelSummaryRankValue.gameObject, false);
            return;
        }

        NGUITools.SetActive(m_labelRankText.gameObject, true);
        NGUITools.SetActive(m_labelRankValue.gameObject, true);
        NGUITools.SetActive(m_labelSummaryRankText.gameObject, true);
        NGUITools.SetActive(m_labelSummaryRankValue.gameObject, true);

        m_labelRankValue.text = m_cachedLeaderboardRank.ToString();
        m_labelSummaryRankValue.text = m_cachedLeaderboardRank.ToString();
    }

    public void QueryLeaderboardRank()
    {
        if (!Ssg.Social.Social.GetInstance().IsAuthenticated)
            return;

        Ssg.Social.Social.GetInstance().GetLocalUserScore(SocialIds.LeaderboardSuisSaved, (score) =>
        {
            m_cachedLeaderboardRank = 0;
            if (score != null)
                m_cachedLeaderboardRank = score.Rank;

            UpdatePlayerRank();
        });
    }

    void Awake()
    {
        NGUITools.SetActive(m_summaryView.gameObject, false);
        m_superheroController.Started += M_superheroController_Started;

        m_summaryView.ContinueClicked += () => { ContinueWithAd(); };
        m_summaryView.PlayAgainClicked += () => { RestartGame(false); };

        m_pauseView.Gameplay = this;

        m_version.text = "v" + Application.version;

        UpdateSpeakerIcon();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused && m_state != null && m_state.IsPauseable && !IsPaused)
        {
            Pause();
        }
    }

    private void M_superheroController_Started()
    {
        ChangeState(new GameplayStatePlay(this));

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Waves", 0);

        NGUITools.SetActive(m_pauseButton.gameObject, true);
        Invoke("StartGame", 0.6f);
    }

    void StartGame()
    {
        ShowCurrentWaveTitle();
        Invoke("StartWave", 1.2f);
    }

    void Start()
    {
        SetBridgeColor();

        m_earthQuakeCinematic.Init(m_cameraShaker);

        if (m_playAgain)
        {
            m_playAgain = false;
            ChangeState(new GameplayStateTutorial(this));
        }
        else
            SetStartScreen();

        start_time = Time.time;

        m_currentWaveLength = GetWaveLength(m_waveNumber);
        m_waitingForNextWave = true;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        NGUITools.SetActive(m_pauseView.gameObject, true);
    }

    public bool IsPaused
    {
        get { return m_pauseView.gameObject.activeSelf; }
    }

    public void SubmitScores()
    {
        var social = Ssg.Social.Social.GetInstance();

        if (!social.IsAuthenticated)
            return;

        social.ReportLocalUserScore(SocialIds.LeaderboardSuisSaved, PlayerPrefs.GetInt("record", 0), (success) =>
        {
            if (success)
                QueryLeaderboardRank();
        });
        social.ReportLocalUserScore(SocialIds.LeaderboardTotalSuisSaved, PlayerPrefs.GetInt("total", 0), null);
    }

    internal void NextWave()
    {
        GameSettings.SuiDeathsCount = 0;

        m_waveNumber++;
        m_currentWaveLength = GetWaveLength(m_waveNumber);
        m_currentWaveTime = 0.0f;

        ShowCurrentWaveTitle();
        Invoke("StartWave", 1.2f);
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
        if (Time.timeScale != 0.0f)
        {
            if (Input.GetKey(KeyCode.Space))
                Time.timeScale = 5.0f;
            else
                Time.timeScale = 1.0f;
        }

        if (m_state != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                m_state.BackButtonPressed();

            m_state.Update();
            return;
        }
    }

    public void EndRound()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Waves", GameSettings.SuiRescuedCount);
        GameAnalytics.NewDesignEvent("WavesCompleted", m_waveNumber);
        GameAnalytics.NewDesignEvent("GameOver", GameSettings.SuiRescuedCount);

        Time.timeScale = 0.0f;

        AudioManager.GetInstance().SoundSummary.Play();

        NGUITools.SetActive(m_summaryView.gameObject, true);
        m_summaryView.Show();

        NGUITools.SetActive(m_waveIndicator.gameObject, false);
        m_isRoundEnded = true;

        NGUITools.SetActive(m_pauseButton.gameObject, false);
    }

    public void RestartGame(bool goToDesktop)
    {
        GameAnalytics.NewDesignEvent("PlayAgain", GameSettings.SuiRescuedCount);

        m_playAgain = !goToDesktop;

        Time.timeScale = 1.0f;

        DestroySuperhero();
        DestroySuiciders();

        GameSettings.Restart();
        SceneManager.LoadScene("GameplayTest");
    }

    public void ContinueWithAd()
    {
        GameAnalytics.NewDesignEvent("ContinueWithAd", GameSettings.SuiRescuedCount);

        RewardedAds.GetInstance().Play((args) =>
        {
            if (args.Result == Ssg.Ads.AdFinishedEventArgs.ResultType.FullyWatched)
            {
                Continue();
            }
        });
    }

    public void Continue()
    {
        m_isRoundEnded = false;
        m_state = new GameplayStatePlay(this);

        DestroyFallingAndSinkingSuis();

        NGUITools.SetActive(m_summaryView.gameObject, false);
        NGUITools.SetActive(m_pauseButton.gameObject, true);
        GameSettings.SuiDeathsCount = 0;
        Time.timeScale = 1.0f;
    }

    private void DestroyFallingAndSinkingSuis()
    {
        while (SuiControllerFalling.Suiciders.Count > 0)
        {
            SuiControllerFalling.Suiciders[0].Destroy();
        }

        while (SuiControllerSinking.Suiciders.Count > 0)
        {
            SuiControllerSinking.Suiciders[0].Destroy();
        }
    }

    void DestroySuperhero()
    {
        m_superhero.transform.parent.gameObject.SetActive(false);
        //Destroy(m_superhero.transform.parent.gameObject);
        //Destroy(m_superheroController.gameObject);
    }

    void DestroySuiciders()
    {
        m_suiManager.Reset();

        SuiControllerFalling.Reset();
        SuiControllerPreparingForJump.Reset();
        SuiControllerWalkOnBridge.Reset();
        SuiControllerSinking.Reset();
        SuiControllerWalkAway.Reset();
        SuiControllerDiving.Reset();
        SuiControllerWithSuperhero.Reset();
    }

    internal float CalculateSuiDelay(int waveNumber, float waveTime)
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

    internal void UpdateGrabStats()
    {
        int grabCapacity;
        int suiSaved;
        int suiGoal;
        CalcGrabStats(GameSettings.SuiRescuedCount, out grabCapacity, out suiSaved, out suiGoal);

        m_grabPanel.SetGrabCapacity(grabCapacity);
        //GameSettings.HandCapacity = grabCapacity;

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

        //HandCapacityGameSettings.HandCapacity = 3;
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

    public void HandleStartClicked()
    {
        if (m_state != null)
        {
            m_state.HandleStartClicked();
        }
    }

    public void ToggleSound()
    {
        AudioManager.GetInstance().SetSoundsEnabled(!AudioManager.GetInstance().SoundsEnabled);
        UpdateSpeakerIcon();
    }

    private void UpdateSpeakerIcon()
    {
        m_speakerIcon.spriteName = AudioManager.GetInstance().SoundsEnabled ? "SpeakerIcon" : "SpeakerCrossedIcon";
    }

    public void ShowLeaderboards()
    {
        var social = Ssg.Social.Social.GetInstance();
        if (!social.IsAuthenticated)
        {
            social.Authenticate((success) =>
            {
                if (success)
                {
                    ShowLeaderboards();
                    SubmitScores();
                }
            });

            return;
        }

        GameAnalytics.NewDesignEvent("ShowLeaderboard");
        Ssg.Social.Social.GetInstance().ShowLeaderboards();
    }

    public void ShakeCamera(bool shake)
    {
        m_shakeCamera = shake;
    }

    public void UpdateCameraShake()
    {
        if (!m_shakeCamera)
            return;

        Vector3 position = m_cameraShaker.GetShakeValue(Time.deltaTime);
        position.z = 0;
        m_cameraShakeRoot.transform.position = position;
    }

    public void ReportAchievements()
    {
        var social = Ssg.Social.Social.GetInstance();
        if (!social.IsAuthenticated)
            return;
        int totalSavedSuis = GGHeroGame.GetTotal();
        if (totalSavedSuis >= 100)
            social.ReportAchievement(SocialIds.AchievementSave100InTotal);
        if (totalSavedSuis >= 200)
            social.ReportAchievement(SocialIds.AchievementSave200InTotal);
        if (totalSavedSuis >= 500)
            social.ReportAchievement(SocialIds.AchievementSave500InTotal);
        if (totalSavedSuis >= 1000)
            social.ReportAchievement(SocialIds.AchievementSave1000InTotal);
        if (totalSavedSuis >= 5000)
            social.ReportAchievement(SocialIds.AchievementSave5000InTotal);
    }

    public void ReportAchievementIfNeeded(int totalBefore, int totalAfter)
    {
        var social = Ssg.Social.Social.GetInstance();
        if (!social.IsAuthenticated)
            return;
        if (totalBefore < 100 && totalAfter >= 100)
            social.ReportAchievement(SocialIds.AchievementSave100InTotal);
        if (totalBefore < 200 && totalAfter >= 200)
            social.ReportAchievement(SocialIds.AchievementSave200InTotal);
        if (totalBefore < 500 && totalAfter >= 500)
            social.ReportAchievement(SocialIds.AchievementSave500InTotal);
        if (totalBefore < 1000 && totalAfter >= 1000)
            social.ReportAchievement(SocialIds.AchievementSave1000InTotal);
        if (totalBefore < 5000 && totalAfter >= 5000)
            social.ReportAchievement(SocialIds.AchievementSave5000InTotal);
    }

    public void PlayEarthQuakeCinematic()
    {
        m_earthQuakeCinematic.Play();
    }

    private Color GetBridgeColor()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || true)
            return new Color32(111, 193, 78, 255);
        else
            return new Color32(240, 122, 0, 255);
    }

    private void SetBridgeColor()
    {
        var color = GetBridgeColor();

        m_bridgeMaterial.color = color;
    }
}

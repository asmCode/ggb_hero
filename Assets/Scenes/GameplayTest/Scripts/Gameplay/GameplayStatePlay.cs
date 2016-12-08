using UnityEngine;
using System.Collections.Generic;

class GameplayStatePlay : GameplayState
{
    public override bool IsPauseable
    {
        get { return true; }
    }

    public GameplayStatePlay(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
        Gameplay.SetHudVisible(true);
        Gameplay.m_fps.SetActive(GGHeroGame.Debug);
        Gameplay.ReportAchievements();
    }

    public override void Leave()
    {
    }

    public override void Update()
    {
        if (Gameplay.m_isRoundEnded)
            return;

        if (Gameplay.m_superhero.IsOnWater && !Gameplay.m_superhero.IsSwimming)
        {
            NGUITools.SetActive(Gameplay.m_swimmingTutorial, true);
        }
        else
        {
            NGUITools.SetActive(Gameplay.m_swimmingTutorial, false);
        }

        CheckCollisionWithSuises();
        CheckCollisionWithDestroyAreas();

        Gameplay.m_suiManager.m_suiGenerator.SuicidersDelay = Gameplay.CalculateSuiDelay(Gameplay.m_waveNumber, Gameplay.m_currentWaveTime);

        Gameplay.m_shoreArrows.gameObject.SetActive(Gameplay.m_superhero.GetHoldingSuis() > 0);

        if (!Gameplay.m_isRoundEnded && GameSettings.SuiDeathsCount >= GameSettings.SuiDeathsLimit)
        {
            Gameplay.ChangeState(new GameplayStateSummary(Gameplay));
            return;
        }

        Gameplay.m_currentWaveTime += Time.deltaTime;
        if (Gameplay.m_currentWaveTime >= Gameplay.m_currentWaveLength)
        {
            Gameplay.m_suiManager.m_suiGenerator.JumpSuis = false;

            if (!Gameplay.m_waitingForNextWave)
            {
                if (SuiControllerPreparingForJump.Suiciders.Count == 0 &&
                    SuiControllerFalling.Suiciders.Count == 0 &&
                    SuiControllerSinking.Suiciders.Count == 0 &&
                    SuiControllerWithSuperhero.Suiciders.Count == 0)
                {
                    Gameplay.m_waitingForNextWave = true;
                    Gameplay.NextWave();
                }
            }
        }

        Gameplay.UpdateGrabStats();
    }

    public override void BackButtonPressed()
    {
        Gameplay.Pause();
    }

    private void CheckCollisionWithSuises()
    {
        const float SqrCollisionDistance = 0.16f * 0.16f;

        var collidedSuis = new List<Suicider>();

        foreach (var sui in SuiControllerFalling.Suiciders)
        {
            float distance = (Gameplay.m_superhero.transform.position - sui.transform.position).sqrMagnitude;
            if (distance < SqrCollisionDistance)
                collidedSuis.Add(sui);
        }

        foreach (var sui in SuiControllerSinking.Suiciders)
        {
            float distance = (Gameplay.m_superhero.transform.position - sui.transform.position).sqrMagnitude;
            if (distance < SqrCollisionDistance)
                collidedSuis.Add(sui);
        }

        foreach (var sui in collidedSuis)
        {
            Gameplay.m_superhero.NotifyCollisionWithSui(sui);
        }
    }

    private void CheckCollisionWithDestroyAreas()
    {
        var collidedSuis = new List<Suicider>();

        foreach (var sui in SuiControllerDiving.Suiciders)
        {
            foreach (var destroyArea in Gameplay.m_suiDestroyAreas)
            {
                if (destroyArea.IsPointInside(sui.transform.position))
                    collidedSuis.Add(sui);
            }
        }

        foreach(var sui in SuiControllerWalkAway.Suiciders)
        {
            foreach (var destroyArea in Gameplay.m_suiDestroyAreas)
            {
                if (destroyArea.IsPointInside(sui.transform.position))
                    collidedSuis.Add(sui);
            }
        }

        foreach (var sui in collidedSuis)
        {
           sui.NotifyCollisionWithDestroyArea();
        }
    }
}

using UnityEngine;

class GameplayStateStart : GameplayState
{
    public GameplayStateStart(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
        Gameplay.UpdatePlayerRank();
        Gameplay.SetStartScreenVisible(true);

        int record = PlayerPrefs.GetInt("record", 0);
        Gameplay.SetRecord(record);
        Gameplay.QueryLeaderboardRank();
    }

    public override void Leave()
    {
        Gameplay.SetStartScreenVisible(false);
    }

    public override void HandleStartClicked()
    {
        if (GameSettings.Censore)
            Gameplay.ChangeState(new GameplayStateEarthQuake(Gameplay));
        else
            Gameplay.ChangeState(new GameplayStateTutorial(Gameplay));
    }

    public override void Update()
    {

    }

    public override void BackButtonPressed()
    {
        Application.Quit();
    }
}

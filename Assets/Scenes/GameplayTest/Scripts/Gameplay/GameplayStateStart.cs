using UnityEngine;

class GameplayStateStart : GameplayState
{
    public GameplayStateStart(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
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
        Gameplay.ChangeState(new GameplayStateTutorial(Gameplay));
    }

    public override void Update()
    {

    }
}

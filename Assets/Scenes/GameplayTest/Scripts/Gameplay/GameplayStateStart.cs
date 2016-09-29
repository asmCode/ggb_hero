class GameplayStateStart : GameplayState
{
    public GameplayStateStart(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
        Gameplay.SetStartScreenVisible(true);
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

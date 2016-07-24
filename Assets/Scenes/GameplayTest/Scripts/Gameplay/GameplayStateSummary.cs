class GameplayStateSummary : GameplayState
{
    public GameplayStateSummary(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
        Gameplay.EndRound();
    }

    public override void Update()
    {

    }
}

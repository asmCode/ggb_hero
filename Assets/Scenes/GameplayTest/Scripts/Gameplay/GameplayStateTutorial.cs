class GameplayStateTutorial : GameplayState
{
    public GameplayStateTutorial(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
        Gameplay.SetTutorialVisible(true);
        Gameplay.m_superheroController.gameObject.SetActive(true);
        Gameplay.SetStartScreenVisible(false);
    }

    public override void Leave()
    {
        Gameplay.SetTutorialVisible(false);
    }

    public override void HandleStartClicked()
    {
        Gameplay.ChangeState(new GameplayStateTutorial(Gameplay));
    }

    public override void Update()
    {

    }
}

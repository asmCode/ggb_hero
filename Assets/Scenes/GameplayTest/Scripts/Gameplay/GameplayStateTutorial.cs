class GameplayStateTutorial : GameplayState
{
    public GameplayStateTutorial(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
        Gameplay.SetHudVisible(false);
        Gameplay.SetTutorialVisible(true);
        Gameplay.m_superheroController.gameObject.SetActive(true);
        Gameplay.SetStartScreenVisible(false);
    }

    public override void Leave()
    {
        Gameplay.SetTutorialVisible(false);
    }

    public override void Update()
    {

    }
}

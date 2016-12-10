class GameplayStateEarthQuake : GameplayState
{
    public GameplayStateEarthQuake(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
        Gameplay.m_earthQuakeCinematic.AnimationFinished += HandleAnimationFinished;
        Gameplay.ShakeCamera(true);
        Gameplay.PlayEarthQuakeCinematic();
    }

    public override void Leave()
    {
        Gameplay.m_earthQuakeCinematic.AnimationFinished -= HandleAnimationFinished;
        Gameplay.m_earthQuakeCinematic.gameObject.SetActive(false);
    }

    public override void Update()
    {
        Gameplay.UpdateCameraShake();
    }

    private void HandleAnimationFinished()
    {
        Gameplay.ChangeState(new GameplayStateTutorial(Gameplay));
    }
}

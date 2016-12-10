class GameplayStateEarthQuake : GameplayState
{
    public GameplayStateEarthQuake(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Enter()
    {
        Gameplay.ShakeCamera(true);
        Gameplay.PlayEarthQuakeCinematic();
    }

    public override void Leave()
    {
    }

    public override void Update()
    {
        Gameplay.UpdateCameraShake();
    }
}

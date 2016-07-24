class GameplayState
{
    protected Gameplay Gameplay
    {
        get;
        private set;
    }

    public GameplayState(Gameplay gameplay)
    {
        Gameplay = gameplay;
    }

    public virtual void Enter() { }
    public virtual void Leave() { }
    public virtual void Update() { }
}

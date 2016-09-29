public class GameplayState
{
    public virtual bool IsPauseable
    {
        get
        {
            return false;
        }
    }

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

    public virtual void HandleStartClicked() { }
}

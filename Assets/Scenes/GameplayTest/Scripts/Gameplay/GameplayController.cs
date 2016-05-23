using UnityEngine;
using System.Collections;

public class GameplayController
{
    protected Gameplay Gameplay
    {
        get;
        private set;
    }

    public GameplayController(Gameplay gameplay)
    {
        Gameplay = gameplay;
    }

    public virtual void Start() { }
    public virtual void Update() { }
}

using UnityEngine;
using System.Collections;

public class InfiniteGameplayController : GameplayController
{
    public InfiniteGameplayController(Gameplay gameplay) : base(gameplay)
    {
    }

    public override void Start()
    {
        Gameplay.m_suiManager.m_suiGenerator.SuicidersDelay = 2.0f;
    }

    public override void Update()
    {

    }
}

using UnityEngine;
using System.Collections;

public class AlienReturnEffectPool : Pool<AlienReturnEffect>
{
    public static AlienReturnEffectPool Instance
    {
        get;
        private set;
    }

    public Transform m_container;

    protected override void OnCreated(AlienReturnEffect poolObject)
    {
        poolObject.transform.parent = m_container;
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}

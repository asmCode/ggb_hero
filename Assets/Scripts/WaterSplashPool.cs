using UnityEngine;
using System.Collections;

public class WaterSplashPool : Pool<WaterSplash>
{
    public static WaterSplashPool Instance
    {
        get;
        private set;
    }

    public Transform m_container;

    protected override void OnCreated(WaterSplash poolObject)
    {
        poolObject.transform.parent = m_container;
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}

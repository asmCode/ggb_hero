using UnityEngine;
using System.Collections;

public class FallingBridgeElementPool : Pool<FallingBridgeElement>
{
    public static FallingBridgeElementPool Instance
    {
        get;
        private set;
    }

    public Transform m_container;
    public WaterLevel m_waterLevel;

    protected override void OnCreated(FallingBridgeElement poolObject)
    {
        poolObject.transform.SetParent(m_container);
        poolObject.m_waterLevel = m_waterLevel;
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}

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
    public Water m_water;

    protected override void OnCreated(FallingBridgeElement poolObject)
    {
        poolObject.transform.SetParent(m_container);
        poolObject.m_water = m_water;
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}

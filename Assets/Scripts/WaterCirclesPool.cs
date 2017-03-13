using UnityEngine;
using System.Collections;

public class WaterCirclesPool : Pool<WaterCircles>
{
    public static WaterCirclesPool Instance
    {
        get;
        private set;
    }

    protected override void OnCreated(WaterCircles poolObject)
    {
        poolObject.transform.SetParent(transform);
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}

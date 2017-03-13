using UnityEngine;
using System.Collections;

public class WaterCirclesAnimationPool : Pool<WaterCirclesAnimation>
{
    public static WaterCirclesAnimationPool Instance
    {
        get;
        private set;
    }

    protected override void OnCreated(WaterCirclesAnimation poolObject)
    {
        poolObject.transform.SetParent(transform);
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}

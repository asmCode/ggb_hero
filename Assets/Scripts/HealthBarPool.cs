using UnityEngine;
using System.Collections;

public class HealthBarPool : Pool<HealthBar>
{
    public static HealthBarPool Instance
    {
        get;
        private set;
    }

    public Transform m_container;

    protected override void OnCreated(HealthBar poolObject)
    {
        poolObject.transform.parent = m_container;
        poolObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
    }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}

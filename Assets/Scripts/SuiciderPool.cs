using UnityEngine;
using System.Collections;

public class SuiciderPool : Pool<SuiciderRoot>
{
    public Transform m_container;

    protected override void OnCreated(SuiciderRoot poolObject)
    {
        poolObject.transform.parent = m_container;
    }
}

using UnityEngine;
using System.Collections;

public class SurvivorNamePool : Pool<SurvivorName>
{
    public Transform m_container;

    protected override void OnCreated(SurvivorName poolObject)
    {
        poolObject.transform.parent = m_container;
    }
}

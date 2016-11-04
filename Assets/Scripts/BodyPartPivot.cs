using UnityEngine;
using System.Collections;

public class BodyPartPivot : MonoBehaviour
{
    public Vector3 m_resetPosition;

    public BodyPart BodyPart
    {
        get;
        private set;
    }

    public void Reset()
    {
        transform.localPosition = m_resetPosition;
    }

    private void Awake()
    {
        BodyPart = GetComponentInChildren<BodyPart>();
    }
}

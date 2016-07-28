using UnityEngine;
using System.Collections;

public class BodyPartPivot : MonoBehaviour
{
    public BodyPart BodyPart
    {
        get;
        private set;
    }

    private void Awake()
    {
        BodyPart = GetComponentInChildren<BodyPart>();
    }
}

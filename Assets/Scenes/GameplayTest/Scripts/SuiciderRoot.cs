using UnityEngine;
using System.Collections;

public class SuiciderRoot : MonoBehaviour
{
    public Suicider Suicider
    {
        get;
        private set;
    }

    private void Awake()
    {
        Suicider = GetComponentInChildren<Suicider>();
    }
}

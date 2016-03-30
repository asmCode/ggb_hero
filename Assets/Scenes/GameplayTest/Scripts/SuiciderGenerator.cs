﻿using UnityEngine;
using System.Collections;

public class SuiciderGenerator : MonoBehaviour
{
    public Suicider m_suiciderPrefab;
    public Transform m_suiciderContainer;

    private static readonly float SuicidersDelay = 3.0f;
    private static readonly float StartHeight = 5.0f;
    private static readonly float StartWidth = 16.0f;

    private float m_cooldown;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_cooldown += Time.deltaTime;
        if (m_cooldown >= SuicidersDelay)
        {
            GenerateSuicider();
            m_cooldown -= SuicidersDelay;
        }
    }

    private void GenerateSuicider()
    {
        Vector2 position = new Vector3(
            Random.Range(-StartWidth / 2.0f, StartWidth / 2.0f),
            StartHeight);

        Suicider suicider = Instantiate(m_suiciderPrefab).GetComponent<Suicider>();
        suicider.SetController(new SuiControllerFalling(suicider, position));
        suicider.transform.parent = m_suiciderContainer;
    }
}

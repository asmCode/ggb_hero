using UnityEngine;
using System.Collections;

public class SuiciderGenerator : MonoBehaviour
{
    public Suicider m_suiciderPrefab;

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
        Suicider suicider = Instantiate(m_suiciderPrefab).GetComponent<Suicider>();
        suicider.transform.position = new Vector3(
            Random.Range(-StartWidth / 2.0f, StartWidth / 2.0f),
            StartHeight,
            0.0f);
    }
}

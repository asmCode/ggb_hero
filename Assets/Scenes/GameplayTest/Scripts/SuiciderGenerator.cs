using UnityEngine;
using System.Collections;

public class SuiciderGenerator : MonoBehaviour
{
    public Suicider m_suiciderPrefab;
    public Transform m_suiciderContainer;
    public Transform m_jumpArea;
    public Transform m_bridgeHeight;
    public Transform m_spawnPoint;
    public Water m_water;

    private static readonly float SuicidersDelay = 2.0f;

    private float m_cooldown;

    // Use this for initialization
    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
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
        Vector2 position = new Vector2(
            Random.Range(0, 2) == 0 ? m_spawnPoint.transform.position.x : -m_spawnPoint.transform.position.x,
            m_bridgeHeight.position.y);

        Suicider suicider = Instantiate(m_suiciderPrefab).GetComponent<Suicider>();
        suicider.Initialize(m_water);
        suicider.SetController(new SuiControllerWalkOnBridge(suicider, position, GetRandomJumpCoord()));
        suicider.transform.parent = m_suiciderContainer;
    }

    private float GetRandomJumpCoord()
    {
        float min = m_jumpArea.transform.position.x - m_jumpArea.transform.localScale.x / 2.0f;
        float max = m_jumpArea.transform.position.x + m_jumpArea.transform.localScale.x / 2.0f;

        return Random.Range(min, max);
    }
}

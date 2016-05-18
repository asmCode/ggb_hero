using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiciderGenerator : MonoBehaviour
{
    public GameObject m_suiciderPrefab;
    public Transform m_jumpArea;
    public Transform m_bridgeHeight;
    public Transform m_spawnPoint;
    public Water m_water;
    public RectBounds m_bridgeWalkArea;

    private static readonly float SuicidersDelay = 2.0f;

    //private List<Suicider> m_suiciders = new List<Suicider>();
    private float m_cooldown;

    private float BridgeHeight
    {
        get { return m_bridgeHeight.transform.position.y; }
    }

    public void Reset()
    {
        Object[] suiciders = FindObjectsOfType(typeof(Suicider));
        foreach (Suicider sui in suiciders)
        {
            Destroy(sui.transform.parent.gameObject);
        }      
    }

    // Use this for initialization
    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;

        Prewarm(10);
    }

    // Update is called once per frame
    void Update()
    {
        m_cooldown += Time.deltaTime;
        if (m_cooldown >= SuicidersDelay)
        {
            float xCoord = GetRandomOutOfViewSpawnCoord();
            float direction = -Mathf.Sign(xCoord);
            GenerateSuicider(xCoord, direction);
            m_cooldown -= SuicidersDelay;
        }
    }

    private void GenerateSuicider(float xCoord, float direction)
    {
        Vector3 initialPosition = new Vector3(xCoord, BridgeHeight, 0.0f);
        GameObject suiciderGroup = (GameObject)Instantiate(m_suiciderPrefab, initialPosition, Quaternion.identity);
        Transform suiciderBody = suiciderGroup.transform.FindChild("SuiBody");
        Suicider suicider = suiciderBody.GetComponent<Suicider>();
        suicider.Initialize(m_water);
        suicider.SetController(new SuiControllerWalkOnBridge(suicider, initialPosition, direction, m_bridgeWalkArea));
    }

    private void Prewarm(int suiCount)
    {
        for (int i = 0; i < suiCount; i++)
        {
            GenerateSuicider(GetRandomSpawnCoord(), Mathf.Sign(Random.value - 0.5f));
        }
    }

    private float GetRandomSpawnCoord()
    {
        return Mathf.Lerp(-m_spawnPoint.transform.position.x, m_spawnPoint.transform.position.x, Random.value);
    }

    private float GetRandomOutOfViewSpawnCoord()
    {
        return Random.Range(0, 2) == 0 ? m_spawnPoint.transform.position.x : -m_spawnPoint.transform.position.x;
    }
}

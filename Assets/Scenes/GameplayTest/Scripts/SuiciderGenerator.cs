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

    private static readonly float SuicidersDelay = 2.0f;

    //private List<Suicider> m_suiciders = new List<Suicider>();
    private float m_cooldown;

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

        GameObject suiciderGroup = (GameObject)Instantiate(m_suiciderPrefab, new Vector3(position.x, position.y, 0.0f), Quaternion.identity);
        Transform suiciderBody = suiciderGroup.transform.FindChild("SuiBody");
        Suicider suicider = suiciderBody.GetComponent<Suicider>();
        suicider.Initialize(m_water);
        suicider.SetController(new SuiControllerWalkOnBridge(suicider, position, GetRandomJumpCoord()));
    }

    private float GetRandomJumpCoord()
    {
        float min = m_jumpArea.transform.position.x - m_jumpArea.transform.localScale.x / 2.0f;
        float max = m_jumpArea.transform.position.x + m_jumpArea.transform.localScale.x / 2.0f;

        return Random.Range(min, max);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuiciderGenerator : MonoBehaviour
{
    public SuiciderPool m_suiciderPool;
    public RectBounds m_jumpArea;
    public Transform m_bridgeHeight;
    public Transform m_spawnPoint;
    public Water m_water;
    public RectBounds m_bridgeWalkArea;

    private const int WalkingSuisCount = 20;

    private float m_cooldown;

    public float SuicidersDelay
    {
        get;
        set;
    }

    private float BridgeHeight
    {
        get { return m_bridgeHeight.transform.position.y; }
    }

    public void Reset()
    {
        Object[] suiciders = FindObjectsOfType(typeof(Suicider));
        foreach (Suicider sui in suiciders)
        {
            sui.Destroy();
        }
    }

    public bool JumpSuis
    {
        get;
        set;
    }

    // Use this for initialization
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        Prewarm(WalkingSuisCount);
        JumpSuis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (JumpSuis)
        {
            m_cooldown -= Time.deltaTime;
            if (m_cooldown <= 0.0f)
            {
                m_cooldown += SuicidersDelay;
                JumpRandomSui();
            }
        }

        if (SuiControllerWalkOnBridge.Suiciders.Count < WalkingSuisCount)
        {
            float xCoord = GetRandomOutOfViewSpawnCoord();
            float direction = -Mathf.Sign(xCoord);
            GenerateSuicider(xCoord, direction);
        }
    }

    private void GenerateSuicider(float xCoord, float direction)
    {
        Vector3 initialPosition = new Vector3(xCoord, BridgeHeight, 0.0f);
        SuiciderRoot suiciderRoot = m_suiciderPool.Get();
        if (suiciderRoot == null)
            return;

        suiciderRoot.transform.position = initialPosition;
        Suicider suicider = suiciderRoot.Suicider;
        suicider.TintColor = ColorPalette.GetRandomColor();
        suicider.SkinTintColor = ColorPalette.GetRandomSkinColor();
        suicider.Initialize(m_water);
        suicider.SortOrder = 21;
        suicider.SetIsFemale(Random.value > 0.5f);
        suicider.SetController(new SuiControllerWalkOnBridge(suicider, initialPosition, direction, m_bridgeWalkArea));
        suicider.Reset();
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

    private void JumpRandomSui()
    {
        Suicider sui = GetRandomWalkingSuiInsideJumpArea();
        if (sui == null)
            return;

        sui.SetController(new SuiControllerPreparingForJump(sui));
    }

    private Suicider GetRandomWalkingSuiInsideJumpArea()
    {
        List<Suicider> suisInJumpArea = SuiControllerWalkOnBridge.Suiciders.FindAll(t => { return m_jumpArea.IsCoordInsideHori(t.transform.position.x); });
        if (suisInJumpArea.Count == 0)
            return null;

        return suisInJumpArea[Random.Range(0, suisInJumpArea.Count)];
    }
}

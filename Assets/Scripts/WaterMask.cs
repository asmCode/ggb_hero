using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMask : MonoBehaviour
{
    public SpriteRenderer[] m_spriteRenderers;
    public WaterLevel m_waterLevel;
    public bool m_waterLevelNoise = true;

    private int m_uniformId;

    private void Awake()
    {
        m_uniformId = Shader.PropertyToID("_WaterLevel");
        m_waterLevel = GameObject.Find("WaterLevel").GetComponent<WaterLevel>();
    }

    private void Update()
    {
        float level = m_waterLevel.GetWaterHeight(transform.position.x, m_waterLevelNoise);
        level += 0.02f;

        foreach (var item in m_spriteRenderers)
        {
            item.material.SetFloat(m_uniformId, level);
        }
    }
}

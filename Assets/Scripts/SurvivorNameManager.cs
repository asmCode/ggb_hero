using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SurvivorNameManager : MonoBehaviour
{
    private class Survivor
    {
        public string name;
        public Color color;
        public Vector3 position;
        public Transform container;
    }

    public SurvivorNamePool m_pool;

    private Queue<Survivor> m_survivors = new Queue<Survivor>();
    private const float m_cooldown = 0.1f;
    private float m_delay = 0.0f;

    public void Awake()
    {
    }

    public void SetName(string name, Color color, Vector3 position, Transform container)
    {
        var sur = new Survivor();
        sur.name = name;
        sur.color = color;
        sur.position = position;
        sur.container = container;
        m_survivors.Enqueue(sur);
    }

    private void ShowNextName()
    {
        if (m_survivors.Count == 0)
            return;

        var survivor_name = m_pool.Get();
        if (survivor_name == null)
            return;

        var sur = m_survivors.Dequeue();
        survivor_name.gameObject.transform.parent = sur.container;
        survivor_name.transform.OverlayPosition(sur.position, Camera.main);
        survivor_name.transform.localScale = new Vector3(1, 1, 1);
        survivor_name.SetName(sur.name, sur.color);

        AudioManager.GetInstance().SoundSave.Play();
    }

    private void Update()
    {
        m_delay = Mathf.Max(0, m_delay - Time.deltaTime);

        if (m_survivors.Count > 0 && m_delay == 0)
        {
            ShowNextName();
            m_delay = m_cooldown;
        }
    }
}

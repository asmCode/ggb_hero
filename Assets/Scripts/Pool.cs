using UnityEngine;
using System.Collections;

public class Pool<T> : MonoBehaviour
    where T : Component
{
    public T m_prefab;
    public int m_count;

    private T[] m_objects;

    public T Get()
    {
        for (int i = 0; i < m_objects.Length; i++)
        {
            if (!m_objects[i].gameObject.activeSelf)
                m_objects[i].gameObject.SetActive(true);
                return m_objects[i];
        }

        return null;
    }

    private void Awake()
    {
        m_objects = new T[m_count];
        for (int i = 0; i < m_count; i++)
        {
            m_objects[i] = Instantiate(m_prefab);
            m_objects[i].gameObject.SetActive(false);
        }
    }
}
